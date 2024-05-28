using BookStorage.Models.Entities.UserEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.UserRepository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public async Task<RetrieveUserEntity> GetUserAsync(int id)
        {
            return await GetAsync<RetrieveUserEntity>("User_SelectById", new { userId = id });
        }

        public async Task<RetrieveUserEntity> GetUserAsync(string email)
        {
            return await GetAsync<RetrieveUserEntity>("User_SelectByEmail", new { email });
        }

        public async Task<string> GetUserPasswordAsync(string email)
        {
            return await GetAsync<string>("UserPassword_Select", new { email });
        }

        public async Task<RetrieveUserEntity> UpdateUserProfileAsync(SaveUserProfileEntity entity)
        {
            return await GetAsync<RetrieveUserEntity>("UserProfile_Update", entity);
        }

        public async Task<bool> UpdateUserAvatarAsync(string storageReference, int userId)
        {
            return await ExecuteAsync("UserAvatar_Update", new
            {
                avatarStorageReference = storageReference,
                userId
            });
        }

        public async Task<bool> SetUserBalanceAsync(int userId, decimal amount)
        {
            return await ExecuteAsync("User_SetBalance", new { userId, amount });
        }
    }
}