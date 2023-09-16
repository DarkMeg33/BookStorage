using BookStorage.Models.Entities.UserEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.UserRepository
{
    public interface IUserRepository : IRepository
    {
        Task<UserEntity> GetUserAsync(int id);
        Task<UserEntity> UpsertUserAsync(UserEntity entity);
    }
}