using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet("account/login")]
        public IActionResult Index(string returnUrl)
        {
            return View();
        }
    }
}
