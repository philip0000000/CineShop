using Microsoft.AspNetCore.Mvc;

namespace CineShop.Controllers
{
    public class OrdersController : Controller
    {
        [Route("customer/orders/{email}")]
        public IActionResult CustomerOrders()
        {
            return View();
        }
        public IActionResult AdminOrders()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
