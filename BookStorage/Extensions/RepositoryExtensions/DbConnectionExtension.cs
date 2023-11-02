using System.Data;
using RepoDb;

namespace BookStorage.Extensions.RepositoryExtensions
{
    public static class DbConnectionExtension
    {
        public static async Task<T> GetAsync<T>(this IDbConnection connection,
            string query, object parameter = null, CommandType commandType = CommandType.StoredProcedure,
            IDbTransaction transaction = null)
        {
            return (await connection.ExecuteQueryAsync<T>(query, parameter, commandType, transaction: transaction))
                .FirstOrDefault();
        }

        public static async Task<IEnumerable<T>> GetAllAsync<T>(this IDbConnection connection, 
            string query, object parameter = null, CommandType commandType = CommandType.StoredProcedure, 
            IDbTransaction transaction = null)
        {
            return await connection.ExecuteQueryAsync<T>(query, parameter, commandType, transaction: transaction);
        }

        public static async Task<bool> ExecuteNonQueryStoredProcedureAsync(this IDbConnection connection,
            string query, object parameter = null,
            IDbTransaction transaction = null)
        {
            return (await connection.ExecuteNonQueryAsync(query,  parameter, 
                CommandType.StoredProcedure, transaction: transaction)) != 0;
        }
    }
}