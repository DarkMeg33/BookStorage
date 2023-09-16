using BookStorage.Models.Entities.UserEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.UserRepository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public async Task<UserEntity> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> UpsertUserAsync(UserEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}