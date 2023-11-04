using System.Security.Claims;
using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.Dto.UserDto;
using BookStorage.Models.Entities.UserEntities;
using BookStorage.Models.ViewModels.AccountViewModel;
using BookStorage.Repositories.AccountRepository;
using BookStorage.Repositories.Base;
using BookStorage.Repositories.UserRepository;
using BookStorage.Services.UserService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BookStorage.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            _accountRepository = accountRepository;
            _userRepository = userRepository;

            _accountRepository.Attach(unitOfWork);
            _userRepository.Attach(unitOfWork);
        }

        public bool IsUserAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User.Identity != null
                   && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<EndpointResultDto> TrySignInAsync(SignInViewModel viewModel)
        {
            Dictionary<string, string> errors = new();

            UserEntity user = await _userRepository.GetUserAsync(viewModel.Email);

            if (user == null)
            {
                errors.Add(nameof(viewModel.Email), "Invalid email or password");
                return new EndpointResultDto(false, errors);
            }

            bool validationPassed = await ValidateUserPassword(viewModel.Email, viewModel.Password);

            if (!validationPassed)
            {
                errors.Add(nameof(viewModel.Email), "Invalid email or password");
                return new EndpointResultDto(false, errors);
            }

            await SetupCookieAsync(new UserDto(user));

            return new EndpointResultDto(true, errors);
        }

        private async Task<bool> ValidateUserPassword(string email, string password)
        {
            string userHashedPassword = await _userRepository.GetUserPasswordAsync(email);

            if (userHashedPassword == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(password, userHashedPassword);
        }

        private async Task SetupCookieAsync(UserDto user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };

            if (_httpContextAccessor?.HttpContext != null)
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                try
                {
                    await _httpContextAccessor.HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal)!;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public async Task<EndpointResultDto> TrySignUpAsync(SignUpViewModel viewModel)
        {
            Dictionary<string, string> errors = new();

            UserEntity existedUser = await _userRepository.GetUserAsync(viewModel.Email);

            if (existedUser != null)
            {
                errors.Add(nameof(viewModel.Email), "Such user is already created.");
                return new EndpointResultDto(false, errors);
            }

            try
            {
                bool result = 
                    await _accountRepository.CreateNewAccountAsync(
                        new SaveNewUserEntity(viewModel));

                return new EndpointResultDto(result, errors);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new EndpointResultDto(false, errors);
            }
        }
    }
}