using System.Data;
using RepoDb;

namespace BookStorage.Extensions.RepositoryExtensions
{
    public static class DbConnectionExtension
    {
        public static async Task<IEnumerable<T>> GetAllAsync<T>(this IDbConnection connection, 
            string query, object parameter = null, CommandType commandType = CommandType.StoredProcedure, 
            IDbTransaction transaction = null)
        {
            return await connection.ExecuteQueryAsync<T>(query, parameter, commandType, transaction: transaction);
        }
    }
}