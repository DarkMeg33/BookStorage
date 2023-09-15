using System.Data;
using BookStorage.Settings;
using Microsoft.Data.SqlClient;
using RepoDb;

namespace BookStorage.Repositories.Base
{
    public class RepoDbUnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;

        private readonly string _connectionString;

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    Begin();
                }

                return _connection;
            }
        }

        public RepoDbUnitOfWork(AppSettings appSettings)
        {
            _connectionString = appSettings.ConnectionStrings.SqlServer;
        }

        private IDbConnection OpenConnection()
        {
            return _connection ??= new SqlConnection(_connectionString).EnsureOpen();
        }

        public void Begin()
        {
            OpenConnection();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void RollBack()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string queryString, object parameter = null, CommandType commandType = CommandType.StoredProcedure)
        {
            return await Connection.ExecuteQueryAsync<T>(queryString, parameter, commandType);
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
    }
}