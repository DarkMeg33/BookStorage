using BookStorage.Models.Entities.BookEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.BookRepository
{
    public interface IBookRepository : IRepository
    {
        Task<List<RetrieveBookEntity>> GetBooksAsync();
        Task<RetrieveBookEntity> GetBookAsync(int bookId);
        Task<RetrieveBookEntity> GetBookAsync(string title);
        Task<RetrieveBookEntity> UpsertBookAsync(SaveBookEntity entity);
    }
}