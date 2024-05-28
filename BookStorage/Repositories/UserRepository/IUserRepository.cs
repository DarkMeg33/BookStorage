using BookStorage.Models.Entities.UserEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.UserRepository
{
    public interface IUserRepository : IRepository
    {
        Task<RetrieveUserEntity> GetUserAsync(int id);
        Task<RetrieveUserEntity> GetUserAsync(string email);
        Task<string> GetUserPasswordAsync(string email);
        Task<RetrieveUserEntity> UpdateUserProfileAsync(SaveUserProfileEntity entity);
        Task<bool> UpdateUserAvatarAsync(string storageReference, int userId);
        Task<bool> SetUserBalanceAsync(int userId, decimal amount);
    }
}