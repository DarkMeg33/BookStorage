namespace BookStorage.Services.ClaimService
{
    public interface IClaimService
    {
        TResult GetClaimValue<TResult>(string claimType);
    }
}