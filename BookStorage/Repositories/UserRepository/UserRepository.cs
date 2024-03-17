using BookStorage.Models.Entities.UserEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.UserRepository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public async Task<UserEntity> GetUserAsync(int id)
        {
            return await GetAsync<UserEntity>("User_SelectById", new { userId = id });
        }

        public async Task<UserEntity> GetUserAsync(string email)
        {
            return await GetAsync<UserEntity>("User_SelectByEmail", new { email });
        }

        public async Task<string> GetUserPasswordAsync(string email)
        {
            return await GetAsync<string>("UserPassword_Select", new { email });
        }

        public async Task<UserEntity> UpdateUserProfileAsync(SaveUserProfileEntity entity)
        {
            return await GetAsync<UserEntity>("UserProfile_Update", entity);
        }
    }
}