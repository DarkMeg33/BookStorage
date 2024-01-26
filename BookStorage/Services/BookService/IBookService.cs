using BookStorage.Models.Dto.BookDto;
using BookStorage.Models.ViewModels.BookViewModel;

namespace BookStorage.Services.BookService
{
    public interface IBookService
    {
        Task<List<BookDto>> GetBookDtosAsync();
        Task<GetBookDto> GetBookDtoAsync(int bookId);
        Task<BookViewModel> GetBookViewModelAsync(int bookId);
    }
}