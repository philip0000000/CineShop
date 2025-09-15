
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace CineShop.Controllers
{
    // Internal test model
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class AccountController : Controller
    {
        // Hardcoded test users
        private static readonly List<User> TestUsers = new()
        {
            new User { Email = "admin@cinema.com", Password = "admin123", Role = "admin" },
            new User { Email = "customer@cinema.com", Password = "customer123", Role = "customer" }
        };

        
        [HttpGet]
        public IActionResult Login() => View();

       
        [HttpPost]
        public IActionResult Login(User user)
        {
            var matchedUser = TestUsers.FirstOrDefault(u =>
                u.Email == user.Email && u.Password == user.Password);

            if (matchedUser != null)
            {
                HttpContext.Session.SetString("UserEmail", matchedUser.Email);
                HttpContext.Session.SetString("UserRole", matchedUser.Role);

                return RedirectToAction("LoginSuccess");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View(user);
        }

        
        public IActionResult LoginSuccess()
        {
            ViewBag.Email = HttpContext.Session.GetString("UserEmail");
            ViewBag.Role = HttpContext.Session.GetString("UserRole");
            return View();
        }

        
        [HttpGet]
        public IActionResult Register() => View();

       
        [HttpPost]
        public IActionResult Register(User user)
        {
            // For testing, we won't save the user — just redirect
            return RedirectToAction("Login");
        }

        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        
        public IActionResult AccessDenied() => View();
    }

   
}
