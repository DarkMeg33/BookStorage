using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    public class HomeController : BaseController
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult Index()
        {
            return View();
        }
    }
}