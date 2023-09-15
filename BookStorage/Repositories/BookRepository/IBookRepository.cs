using BookStorage.Models.Entities.BookEntities;

namespace BookStorage.Repositories.BookRepository
{
    public interface IBookRepository
    {
        Task<List<RetrieveBookEntity>> GetBooksAsync();
    }
}