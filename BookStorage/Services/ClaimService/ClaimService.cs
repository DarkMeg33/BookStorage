using System.Security.Claims;

namespace BookStorage.Services.ClaimService
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContext;

        public ClaimService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public TResult GetClaimValue<TResult>(string claimType)
        {
            IEnumerable<Claim> userClaims = _httpContext.HttpContext?.User.Claims;

            if (userClaims == null)
            {
                return default;
            }

            foreach (Claim userClaim in userClaims)
            {
                if (userClaim.Type == claimType)
                {
                    return ChangeType<TResult>(userClaim.Value);
                }
            }

            return default;
        }

        private static T ChangeType<T>(object value)
        {
            Type t = typeof(T);

            return (T)Convert.ChangeType(value, t);
        }
    }
}