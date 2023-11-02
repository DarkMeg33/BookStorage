using BookStorage.Models.Entities.UserEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.AccountRepository
{
    public interface IAccountRepository : IRepository
    {
        Task<bool> CreateNewAccountAsync(SaveNewUserEntity newUser);
    }
}