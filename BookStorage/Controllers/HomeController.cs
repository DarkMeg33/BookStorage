using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}