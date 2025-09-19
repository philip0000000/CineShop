using CineShop.Services;
using CineShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CineShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cart;

        public CartController(ICartService cart)
        {
            _cart = cart;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var items = _cart.GetItems();
            ViewBag.Total = _cart.GetTotal();
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int movieId)
        {
            _cart.Add(movieId);
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int movieId)
        {
            _cart.Remove(movieId);
            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new CartCheckoutViewModel());
        }
    }
}
