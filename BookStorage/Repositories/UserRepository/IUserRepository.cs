using BookStorage.Models.Entities.UserEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.UserRepository
{
    public interface IUserRepository : IRepository
    {
        Task<UserEntity> GetUserAsync(int id);
        Task<UserEntity> GetUserAsync(string email);
        Task<string> GetUserPasswordAsync(string email);
        Task<UserEntity> UpdateUserProfileAsync(SaveUserProfileEntity entity);
    }
}