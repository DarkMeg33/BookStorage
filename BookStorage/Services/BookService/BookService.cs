using BookStorage.Models.Dto.BookDto;
using BookStorage.Repositories.BookRepository;

namespace BookStorage.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<BookDto>> GetBooksAsync()
        {
            return (await _bookRepository.GetBooksAsync())
                .Select(x => new BookDto(x))
                .ToList();
        }
    }
}