using AspNetCoreGeneratedDocument;
using CineShop.Models;
using CineShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Threading.Tasks;

namespace CineShop.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ICustomerService _customers;
        

        public AdminController(IAdminService adminService, ICustomerService customers)
        {
            _adminService = adminService;
            _customers = customers;
            
        }

       //Admin Dashboard

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("UserRole");
                                                                  
            return View();
        }

        // Manage movies

        public async Task<IActionResult> Movies()
        {
            var movies = await _adminService.GetAllMoviesAsync();
            return View(movies);
        }

        //[Authorize]
        public async Task<IActionResult> EditMovie(int id)
        {
            var movie = await _adminService.GetMovieByIdAsync(id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> EditMovie(Movie movie)
        {
            if (!ModelState.IsValid) return View(movie);
            await _adminService.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Movies));
        }

        //[Authorize]
        public IActionResult AddMovie() => View();

        [HttpPost]
        public async Task<IActionResult> AddMovie(Movie movie)
        {
            if (!ModelState.IsValid) return View(movie);
            await _adminService.AddMovieAsync(movie);
            return RedirectToAction(nameof(Movies));
        }

        
         //GET: Admin/DeleteMovie/5
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _adminService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Admin/DeleteMovie/5
        [HttpPost, ActionName("DeleteMovie")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMovieConfirmed(int id)
        {
            await _adminService.DeleteMovieAsync(id);
            TempData["Message"] = "Movie deleted successfully.";
            return RedirectToAction(nameof(Movies));
        }


        //Manage Customers

        //[Authorize]

        //Shows list of customers
        public async Task<IActionResult> Customers()
        {
            var customers = await _adminService.GetAllCustomersAsync();
            return View(customers);
        }

        //Filips code from Customer Controller
        //Changed Name from Register() to AddCustomer()
        //DONE
        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomer(Customer model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check for duplicate email
            if (!string.IsNullOrWhiteSpace(model.EmailAddress) &&
                await _customers.EmailExistsAsync(model.EmailAddress))
            {
                ModelState.AddModelError(nameof(model.EmailAddress), "Email already exists.");
                return View(model);
            }

            await _customers.CreateAsync(model);
            TempData["Message"] = "Customer successfully created.";

            // Added redirect to Admin/Customers after creation
            return RedirectToAction("Customers", "Admin");
        }


        //Filips Code from Customer Controller changed name Edit() to EditCustomer()
        /// <summary>
        /// Displays the edit form for the specified customer.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> EditCustomer(int id)
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
        public async Task<IActionResult> EditCustomer(int id, Customer model)
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


        //DONE
        /// <summary>
        /// Show main info about the customer id.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> CustomerDetails(int id)
        {
            var customer = await _customers.GetByIdWithOrdersAsync(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }



        //[Authorize] Change status to inactive instead for delete
        
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _adminService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

       
        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCustomerConfirmed(int id)
        {
            await _adminService.DeleteCustomerAsync(id);
            TempData["Message"] = "Customer deleted successfully.";
            return RedirectToAction(nameof(Customers));
        }


        // --- ORDERS OVERVIEW---

        //[Authorize]
        //Shows list of Orders
        public IActionResult Orders()///DONE
        {
            var orders = _adminService.GetAllOrdersAsync().Result;
            return View(orders);
        }

        //TO DO
        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _adminService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return View(order);
        }

        ////[Authorize]

        //public async Task<IActionResult> DeleteOrder(int id)
        //{
        //    await _adminService.DeleteOrderAsync(id);
        //    return RedirectToAction(nameof(Orders));
        //}
    }
}
