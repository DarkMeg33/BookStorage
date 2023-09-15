using BookStorage.Models.Entities.BookEntities;

namespace BookStorage.Repositories.BookRepository
{
    public class BookRepository : IBookRepository
    {
        public async Task<List<RetrieveBookEntity>> GetBooksAsync()
        {
            return new List<RetrieveBookEntity>();
        }
    }
}