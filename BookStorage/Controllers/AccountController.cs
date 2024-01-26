using BookStorage.Models.ViewModels.AccountViewModel;
using BookStorage.Services.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    [AllowAnonymous]
    [Route("/account")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("login")]
        public IActionResult SignInUp([FromQuery] string returnUrl)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToHome();
            }

            return View();
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            return DynamicResultResponse(await _accountService.TrySignUpAsync(viewModel));
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            return DynamicResultResponse(await _accountService.TrySignInAsync(viewModel));
        }
        
        [Route("sign-out")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (_accountService.IsUserAuthenticated())
            {
                await _accountService.SignOutAsync();
                return RedirectToHome();
            }

            return ErrorResponse(null);
        }
    }
}
