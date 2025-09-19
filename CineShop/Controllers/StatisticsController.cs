using CineShop.Models;
using CineShop.Services;
using CineShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CineShop.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public IActionResult Index()
        {
            var model = new HomeStatisticsViewModel
            {
                MostPopularMovies = _statisticsService.GetFiveMostPopularMovies(5),
                NewestMovies = _statisticsService.GetFiveNewestMovies(5),
                OldestMovies = _statisticsService.GetFiveOldestMovies(5),
                CheapestMovies = _statisticsService.GetFiveCheapestMovies(5),
                TopSpendingCustomer = _statisticsService.GetTopCustomerByTotalSpend()
                
            };
            return View(model);
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
