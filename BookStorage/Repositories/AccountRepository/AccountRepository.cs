using BookStorage.Models.Entities.UserEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.AccountRepository
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public async Task<bool> CreateNewAccountAsync(SaveNewUserEntity newUser)
        {
            return await ExecuteAsync("Account_Insert", newUser);
        }
    }
}