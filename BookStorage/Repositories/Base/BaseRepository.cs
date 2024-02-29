using System.Data;

namespace BookStorage.Repositories.Base
{
    public class BaseRepository : UnitOfWorkRepository
    {
        public async Task<T> GetAsync<T>(string procedureName, object paramObj = null, 
            int? commandTimeout = null) where T : class
        {
            try
            {
                return (await _unitOfWork.GetAsync<T>(
                    procedureName,
                    parameter: paramObj,
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: commandTimeout));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<T>> GetAllAsync<T>(string procedureName, object paramObj = null, 
            int? commandTimeout = null) where T : class
        {
            try
            {
                return (await _unitOfWork.GetAllAsync<T>(
                        procedureName, 
                        parameter: paramObj, 
                        commandType: CommandType.StoredProcedure,
                        commandTimeout: commandTimeout))
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> ExecuteAsync(string procedureName, object paramObj = null, int? commandTimeout = null)
        {
            try
            {
                return await _unitOfWork.ExecuteNonQueryStoredProcedureAsync(
                    procedureName,
                    parameter: paramObj,
                    commandTimeout: commandTimeout);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}