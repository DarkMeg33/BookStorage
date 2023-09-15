using BookStorage.Services.BookService;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    public class BookController : Controller
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
    }
}
