using System.Data;

namespace BookStorage.Repositories.Base
{
    public class BaseRepository : UnitOfWorkRepository
    {
        public async Task<List<T>> GetAllAsync<T>(string procedureName, object paramObj = null)
        {
            try
            {
                return (await _unitOfWork.GetAllAsync<T>(
                        procedureName, 
                        parameter: paramObj, 
                        commandType: CommandType.StoredProcedure))
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}