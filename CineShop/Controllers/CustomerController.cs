using CineShop.DataBase;
using CineShop.Models;
using CineShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customers;
        private readonly IMovieService _movieService;

        public CustomerController(ICustomerService customers, IMovieService movieService)
        {
            _customers = customers;
            _movieService = movieService;
        }

        //Customer panel 
        //Added to be able to create link to customer's actions
        public IActionResult Index() //DAShboard DONE
        {
            return View();
        }

        /////Update my Info

        /// <summary>
        /// show empty registration form
        /// </summary>

        // OBS! MOVED TO LOGIN/REGISTER
        //[HttpGet]
        //public IActionResult Register()//CREATE PATH
        //{
        //    return View();
        //}

        ///// <summary>
        ///// handle registration post
        ///// </summary>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(Customer model)
        //{
        //    if (ModelState.IsValid == false)
        //        return View(model);

        //    // simple duplicate check by email
        //    if (!string.IsNullOrWhiteSpace(model.EmailAddress) &&
        //        await _customers.EmailExistsAsync(model.EmailAddress))
        //    {
        //        ModelState.AddModelError(nameof(model.EmailAddress), "Email already exists.");
        //        return View(model);
        //    }

        //    var id = await _customers.CreateAsync(model);
        //    TempData["Message"] = "Customer created.";
        //    return RedirectToAction(nameof(Details), new { id });
        //}

        /// <summary>
        /// Show main info about the customer id.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Details(int id)//TO DO
        {
            
            var customer = await _customers.GetByIdWithOrdersAsync(id);
            if (customer == null)
                return NotFound();
            return View(customer);
        }


        /// <summary>
        /// Displays the edit form for the specified customer.
        /// </summary>
        /// 
        [HttpGet]
        public async Task<IActionResult> Edit(int id)//TO DO
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

        public async Task<IActionResult> BrowseMovies()//DONE
        {
            var movies = await _movieService.GetAllAsync();
            return View(movies);
        }

        //CART AND ORDER
        /// <summary>
        /// Show all orders for a customer by their email.
        /// </summary>

        [HttpGet("Customer/Orders/{email}")]
        // Note: Commented out because it conflicts with OrdersController.
        // Both used the same route /customer/orders/{email}, which gave errors.
        // Orders are now handled only in OrdersController for clear structure.
        /*[HttpGet("Customer/Orders/{email}
        public async Task<IActionResult> Orders(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return NotFound();

            var customer = await _customers.GetByEmailWithOrdersAsync(email);
            if (customer == null)
                return NotFound();
            return View(customer);
        }

        //TO DO
        //public async Task<IActionResult> OrderDetails(int id)
        //{
        //    var order = await _adminService.GetOrderByIdAsync(id);
        //    if (order == null) return NotFound();
        //    return View(order);
        //}

        //TO DO
        // Testa if we can use carts actions

        //public IActionResult Index()
        //{
        //    var items = _cart.GetItems();
        //    ViewBag.Total = _cart.GetTotal();
        //    return View(items);
        //}

        //[HttpPost, ValidateAntiForgeryToken]
        //public IActionResult Add(int movieId)
        //{
        //    _cart.Add(movieId);
        //    return RedirectToAction("Index", "Cart");
        //}

        //[HttpPost, ValidateAntiForgeryToken]
        //public IActionResult Remove(int movieId)
        //{
        //    _cart.Remove(movieId);
        //    return RedirectToAction("Index", "Cart");
        //}

        }*/

    }
}
