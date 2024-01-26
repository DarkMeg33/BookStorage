using BookStorage.Models.Dto.BookDto;
using BookStorage.Models.ViewModels.BookViewModel;
using BookStorage.Repositories.Base;
using BookStorage.Repositories.BookRepository;

namespace BookStorage.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;

            _bookRepository.Attach(unitOfWork);
        }

        public async Task<List<BookDto>> GetBooksDtosAsync()
        {
            return (await _bookRepository.GetBooksAsync())
                .Select(x => new BookDto(x))
                .ToList();
        }

        public async Task<GetBookDto> GetBookDtoAsync(int bookId)
        {
            return new GetBookDto(await _bookRepository.GetBookAsync(bookId));
        }

        public async Task<BookViewModel> GetBookViewModelAsync(int bookId)
        {
            return new BookViewModel(await _bookRepository.GetBookAsync(bookId));
        }
    }
}