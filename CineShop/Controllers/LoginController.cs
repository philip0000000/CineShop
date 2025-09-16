using CineShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CineShop.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProccessLogin(User user)
        {
            if (user.UserName == "CineSharp" && user.Password == "Sharp123")
            {
                return View("LoginSuccess", user);
            }
            else
            {
                return View("LoginFailure", user);
            }
           
        }

        public IActionResult Register()
        {
            return View();
        }


    }
}
