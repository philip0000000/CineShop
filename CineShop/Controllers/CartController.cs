using CineShop.Services;
using CineShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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

        // (this is needed for "Add" and "Remove" button on "Your Cart" page, to use with JavaScript)
        // GET /Order/AddToCart?id=5
        [HttpGet]
        public IActionResult AddToCart(int id)
        {
            _cart.Add(id);

            var item = _cart.GetItems().FirstOrDefault(x => x.MovieId == id);
            var qty = item?.Quantity ?? 0;
            var line = (item?.Price ?? 0m) * qty;
            var total = _cart.GetTotal();

            return Json(new
            {
                movieId = id,
                qty = qty,
                lineTotal = line.ToString("C", CultureInfo.CurrentCulture),
                cartTotal = total.ToString("C", CultureInfo.CurrentCulture)
            });
        }

        // GET /Order/RemoveFromCart?id=5
        [HttpGet]
        public IActionResult RemoveFromCart(int id)
        {
            _cart.Remove(id);

            var item = _cart.GetItems().FirstOrDefault(x => x.MovieId == id);
            var qty = item?.Quantity ?? 0;
            var line = (item?.Price ?? 0m) * qty;
            var total = _cart.GetTotal();

            return Json(new
            {
                movieId = id,
                qty = qty,
                lineTotal = line.ToString("C", CultureInfo.CurrentCulture),
                cartTotal = total.ToString("C", CultureInfo.CurrentCulture)
            });
        }
    }
}
