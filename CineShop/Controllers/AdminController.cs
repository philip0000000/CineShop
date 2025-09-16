using CineShop.Models;
using CineShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CineShop.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

       //Admin panel 
        public IActionResult Index()//Add validation to access adminapnel
        {
            
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
        public async Task<IActionResult> Customers()
        {
            var customers = await _adminService.GetAllCustomersAsync();
            return View(customers);
        }

        //[Authorize]

        public IActionResult Orders()///DONE
        {
            var orders = _adminService.GetAllOrdersAsync().Result;
            return View(orders);
        }



        /// <summary>
        /// ///////               TO DO/////////////////////////////////////////////
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        //[Authorize]

        public async Task<IActionResult> EditCustomer(int id)
        {
            var customer = await _adminService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            await _adminService.UpdateCustomerAsync(customer);
            return RedirectToAction(nameof(Customers));
        }

        //[Authorize]
        public IActionResult AddCustomer() => View();

        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            await _adminService.AddCustomerAsync(customer);
            return RedirectToAction(nameof(Customers));
        }

        ////[Authorize] Change status to inactive instead for delete
        //public async Task<IActionResult> DeleteCustomer(int id)
        //{
        //    await _adminService.DeleteCustomerAsync(id);
        //    return RedirectToAction(nameof(Customers));
        //}

        // --- ORDERS OVERVIEW---

       



        /// <summary>
        /// //TO DO// DATA is missed , i need some more seed data and randomly creaet ordesr
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize]

        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _adminService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return View(order);
        }

        //[Authorize]

        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _adminService.DeleteOrderAsync(id);
            return RedirectToAction(nameof(Orders));
        }
    }
}
