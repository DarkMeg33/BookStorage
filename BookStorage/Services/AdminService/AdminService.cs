using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.ViewModels.AccountViewModel;
using BookStorage.Repositories.Base;
using BookStorage.Repositories.UserRepository;
using BookStorage.Services.AccountService;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using BookStorage.Models.Entities.UserEntities;
using Microsoft.AspNetCore.Authentication;

namespace BookStorage.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminService(IUserRepository userRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;

            _userRepository.Attach(unitOfWork);
        }

        public async Task<EndpointResultDto> TrySingInAsAdminAsync()
        {
            const string adminEmail = "admin@book.spot.md";

            UserEntity admin = await _userRepository.GetUserAsync(adminEmail);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, admin.UserId.ToString()),
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim(ClaimTypes.Email, admin.Email),
            };

            if (_httpContextAccessor?.HttpContext != null)
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                try
                {
                    await _httpContextAccessor.HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal)!;

                    return new EndpointResultDto(true, null);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return new EndpointResultDto(false, null);
        }
    }
}