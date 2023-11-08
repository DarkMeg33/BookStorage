using BookStorage.Models.Dto.BookDto;
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
            return Json(await _bookService.GetBooksAsync());
        }

        [Route("/books/{bookId}")]
        public async Task<IActionResult> GetBook([FromRoute] int bookId)
        {
            GetBookDto book = await _bookService.GetBookAsync(bookId);

            return View("Book", book);
        }
    }
}
