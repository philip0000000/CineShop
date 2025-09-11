using Microsoft.AspNetCore.Mvc;

namespace CineShop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(int movieId)
        {
            return View();
        }
        public IActionResult Remove(int movieId)
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
