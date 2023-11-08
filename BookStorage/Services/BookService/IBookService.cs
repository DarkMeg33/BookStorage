using BookStorage.Models.Dto.BookDto;

namespace BookStorage.Services.BookService
{
    public interface IBookService
    {
        Task<List<BookDto>> GetBooksAsync();
        Task<GetBookDto> GetBookAsync(int bookId);
    }
}