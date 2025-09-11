using Microsoft.AspNetCore.Mvc;

namespace CineShop.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
