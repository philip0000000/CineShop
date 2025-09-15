using Microsoft.AspNetCore.Mvc;
using CineShop.Services;
using CineShop.Models;

namespace CineShop.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

       
        public IActionResult MostPopular()
        {
            var movies = _statisticsService.GetFiveMostPopularMovies(5);
            return View("MovieList", movies);
        }

     
        public IActionResult Newest()
        {
            var movies = _statisticsService.GetFiveNewestMovies(5);
            return View("MovieList", movies);
        }

   
        public IActionResult Oldest()
        {
            var movies = _statisticsService.GetFiveOldestMovies(5);
            return View("MovieList", movies);
        }

   
        public IActionResult Cheapest()
        {
            var movies = _statisticsService.GetFiveCheapestMovies(5);
            return View("MovieList", movies);
        }

     
        public IActionResult TopCustomer()
        {
            var customer = _statisticsService.GetTopCustomerByTotalSpend();
            return View("CustomerProfile", customer);
        }
    }
}
