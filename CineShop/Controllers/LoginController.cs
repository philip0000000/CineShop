using CineShop.Models;
using CineShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace CineShop.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ICustomerService _customers;


        public LoginController(IAdminService adminService, ICustomerService customers)
        {
            _adminService = adminService;
            _customers = customers;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProccessLogin(User user)
        {
            if (user.UserName == "CineSharp" && user.Password == "Sharp123")
            {
                // Store role in session
                HttpContext.Session.SetString("UserRole", "Admin"); //Added

                return View("LoginSuccess", user);
            }
            else
            {
                return View("LoginFailure", user);
            }
        }

        public IActionResult Logout()
        {
            // Rensa all sessionsdata
            HttpContext.Session.Clear();

            // Valfritt: visa ett meddelande
            TempData["Message"] = "You have been logged out.";

            // Skicka tillbaka till startsidan eller login
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()//CREATE PATH
        {
            return View();
        }

        /// <summary>
        /// handle registration post
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Customer model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            // simple duplicate check by email
            if (!string.IsNullOrWhiteSpace(model.EmailAddress) &&
                await _customers.EmailExistsAsync(model.EmailAddress))
            {
                ModelState.AddModelError(nameof(model.EmailAddress), "Email already exists.");
                return View(model);
            }

            var id = await _customers.CreateAsync(model);
            TempData["Message"] = "Customer created.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
