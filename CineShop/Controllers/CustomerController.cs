using CineShop.DataBase;
using CineShop.Models;
using CineShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customers;

        public CustomerController(ICustomerService customers)
        {
            _customers = customers;
        }

        /// <summary>
        /// show empty registration form
        /// </summary>
        [HttpGet]
        public IActionResult Register()
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

        /// <summary>
        /// Displays the edit form for the specified customer.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customers.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        /// <summary>
        /// Updates the chosen customer using the form data.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            // Try to update the customer. If it does not exist, show NotFound.
            var result = await _customers.UpdateAsync(id, model);
            if (result == false)
                return NotFound();

            TempData["Message"] = "Customer updated.";
            return RedirectToAction(nameof(Details), new { id });
        }

        /// <summary>
        /// Show main info about the customer id.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customers.GetByIdWithOrdersAsync(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        /// <summary>
        /// Show all orders for a customer by their email.
        /// </summary>
        [HttpGet("Customer/Orders/{email}")]
        public async Task<IActionResult> Orders(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return NotFound();

            var customer = await _customers.GetByEmailWithOrdersAsync(email);
            if (customer == null)
                return NotFound();

            return View(customer);
        }
    }
}
