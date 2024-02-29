using System.Data;
using BookStorage.Extensions.RepositoryExtensions;
using BookStorage.Settings;
using Microsoft.Data.SqlClient;
using RepoDb;

namespace BookStorage.Repositories.Base
{
    public class RepoDbUnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private readonly string _connectionString;

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    Begin(true);
                }

                return _connection;
            }
        }

        public IDbTransaction Transaction => _transaction;

        public RepoDbUnitOfWork(AppSettings appSettings)
        {
            _connectionString = appSettings.ConnectionStrings.SqlServer;
        }

        private IDbConnection OpenConnection()
        {
            return _connection ??= new SqlConnection(_connectionString).EnsureOpen();
        }

        public void Begin(bool isAtomic = false)
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException(
                    "Cannot start a new transaction while the existing other one is still open.");
            }

            OpenConnection();

            if (!isAtomic)
            {
                _transaction = Connection.BeginTransaction();
            }
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();

                _transaction = null;
            }
        }

        public void RollBack()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();

                _transaction = null;
            }
        }

        public async Task<T> GetAsync<T>(string query, object parameter = null,
            CommandType commandType = CommandType.StoredProcedure, int? commandTimeout = null)
        {
            return await Connection.GetAsync<T>(query, parameter, commandType, Transaction, commandTimeout);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string query, object parameter = null, 
            CommandType commandType = CommandType.StoredProcedure, int? commandTimeout = null)
        {
            return await Connection.GetAllAsync<T>(query, parameter, commandType, Transaction, commandTimeout);
        }

        public async Task<bool> ExecuteNonQueryStoredProcedureAsync(string query, object parameter = null, 
            int? commandTimeout = null)
        {
            return await Connection.ExecuteNonQueryStoredProcedureAsync(query, parameter, Transaction, commandTimeout);
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
    }
}