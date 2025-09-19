using CineShop.Models;
using CineShop.Services;
using CineShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CineShop.Controllers
{
    public class HomeController : Controller
    {
        
            private readonly IStatisticsService _statisticsService;

            public HomeController(IStatisticsService statisticsService)
            {
                _statisticsService = statisticsService;
            }

            public IActionResult Index()
            {
                var model = new HomeStatisticsViewModel
                {
                    MostPopularMovies = _statisticsService.GetFiveMostPopularMovies(5),                    
                };

                return View(model);
            }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
