using BookStorage.Models.Dto.EndpointResultDto;
using BookStorage.Models.ViewModels.BookViewModel;
using BookStorage.Services.BookService;
using BookStorage.Services.UserContextService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

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
            return Json(await _bookService.GetBookDtosAsync(await _userContextService.GetUserIdAsync()));
        }

        [HttpGet("/book/{bookId}/view")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> BookView([FromRoute] int bookId)
        {
            BookViewModel book = await _bookService.GetBookViewModelAsync(bookId, await _userContextService.GetUserIdAsync());

            return book == null ? NotFound() : View("BookView", book);
        }

        [HttpGet("/book-cover/{storageReference}")]
        public async Task<IActionResult> GetBookCoverAsync(string storageReference)
        {
            return await _bookService.GetBookCoverFileAsync(storageReference);
        }

        [HttpGet("/book")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        public async Task<IActionResult> Book()
        {
            return View("Book");
        }

        [HttpPost("/book")]
        [Authorize]
        public async Task<IActionResult> SaveBook(FormBookViewModel viewModel)
        {
            return DynamicResultResponse(await _bookService.TryUpsertBookAsync(viewModel,
                await _userContextService.GetUserIdAsync()));
        }

        [HttpGet("/book/{bookId}/checkout")]
        [Authorize]
        public async Task<IActionResult> CheckoutBook([FromRoute] int bookId)
        {
            StripeConfiguration.ApiKey = "sk_test_51N9TGaHogSFojZmEABw6FlHqBNPFy7YLZZx9FEn48c0BX2N71u71FTMUUYSM4nPbaaX6eJLTDYRiSsFSbf7IyNFo00TW9u8eFH";
            string domain = "https://localhost:7251";
            SessionCreateOptions options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = "price_1PLqJ4HogSFojZmE1UYEcuGg",
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = domain + $"/book/{bookId}/buy",
                CancelUrl = domain,
            };
            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [HttpGet("/book/{bookId}/buy")]
        public async Task<IActionResult> BuyBook([FromRoute] int bookId)
        {
            EndpointResultDto result = await _bookService.TryBuyBookAsync(bookId, await _userContextService.GetUserIdAsync());

            if (result.IsSuccess)
            {
                return  Redirect($"/book/{bookId}/view");
            }

            return BadRequest(result.Errors);
        }
    }
}
