using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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