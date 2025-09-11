using Microsoft.AspNetCore.Mvc;

namespace CineShop.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }
        public IActionResult AddToCart()
        {
            return View();
        }
    }
}
