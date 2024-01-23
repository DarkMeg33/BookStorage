using System.Security.Claims;
using BookStorage.Services.ClaimService;

namespace BookStorage.Services.UserContextService
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClaimService _claimService;

        public UserContextService(IHttpContextAccessor httpContextAccessor, IClaimService claimService)
        {
            _httpContextAccessor = httpContextAccessor;
            _claimService = claimService;
        }

        public bool IsUserAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User.Identity != null
                   && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public Task<int> GetUserIdAsync()
        {
            return Task.FromResult(GetUserId());
        }

        private int GetUserId()
        {
            return _claimService.GetClaimValue<int>(ClaimTypes.NameIdentifier);
        }
    }
}