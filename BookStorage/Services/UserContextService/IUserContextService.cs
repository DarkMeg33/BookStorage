namespace BookStorage.Services.UserContextService
{
    public interface IUserContextService
    {
        bool IsUserAuthenticated();
        Task<int> GetUserIdAsync();
    }
}