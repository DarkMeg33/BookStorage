using BookStorage.Models.ViewModels.BookViewModel;
using BookStorage.Services.BookService;
using BookStorage.Services.UserContextService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly IUserContextService _userContextService;

        public BookController(IBookService bookService, IUserContextService userContextService)
        {
            _bookService = bookService;
            _userContextService = userContextService;
        }

        [HttpGet("/books")]
        public async Task<IActionResult> GetBooks()
        {
            return Json(await _bookService.GetBookDtosAsync());
        }

        [HttpGet("/book/{bookId}/view")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> BookView([FromRoute] int bookId)
        {
            BookViewModel book = await _bookService.GetBookViewModelAsync(bookId);

            return book == null ? NotFound() : View("BookView", book);
        }

        [HttpGet("/book")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        public async Task<IActionResult> CreateBook()
        {
            return View("Book");
        }

        [HttpPost("/book")]
        [Authorize]
        public async Task<IActionResult> UpsertBook(FormBookViewModel viewModel)
        {
            return DynamicResultResponse(await _bookService.TryUpsertBookAsync(viewModel,
                await _userContextService.GetUserIdAsync()));
        }
    }
}
