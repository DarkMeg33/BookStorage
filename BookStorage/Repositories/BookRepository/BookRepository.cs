using BookStorage.Models.Entities.BookEntities;
using BookStorage.Repositories.Base;
using System.Net;

namespace BookStorage.Repositories.BookRepository
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public async Task<List<RetrieveBookEntity>> GetBooksAsync()
        {
            return await GetAllAsync<RetrieveBookEntity>("Book_Select");
        }

        public async Task<RetrieveBookEntity> GetBookAsync(int bookId)
        {
            return await GetAsync<RetrieveBookEntity>("Book_Select", new { bookId });
        }

        public async Task<RetrieveBookEntity> GetBookAsync(string title)
        {
            return await GetAsync<RetrieveBookEntity>("Book_Select", new { title });
        }

        public async Task<RetrieveBookEntity> UpsertBookAsync(SaveBookEntity entity)
        {
            return await GetAsync<RetrieveBookEntity>("Book_Upsert", entity);
        }
    }
}