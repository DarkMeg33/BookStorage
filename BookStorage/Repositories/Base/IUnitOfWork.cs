using System.Data;

namespace BookStorage.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void RollBack();

        Task<IEnumerable<T>> GetAllAsync<T>(string queryString, object parameter = null,
            CommandType commandType = CommandType.StoredProcedure);
    }
}