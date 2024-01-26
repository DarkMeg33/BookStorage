using BookStorage.Models.Dto.BookDto;
using BookStorage.Models.ViewModels.BookViewModel;
using BookStorage.Services.BookService;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("/books")]
        public async Task<IActionResult> GetBooks()
        {
            return Json(await _bookService.GetBookDtosAsync());
        }

        [HttpGet("/book/{bookId}/view")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetBook([FromRoute] int bookId)
        {
            BookViewModel book = await _bookService.GetBookViewModelAsync(bookId);

            return View("Book", book);
        }
    }
}
