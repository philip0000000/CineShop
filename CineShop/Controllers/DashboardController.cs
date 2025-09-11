using Microsoft.AspNetCore.Mvc;

namespace CineShop.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
