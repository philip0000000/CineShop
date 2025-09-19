using CineShop.Models;
using CineShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineShop.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICartService _cartService;

        public MoviesController(IMovieService movieService, ICartService cartService)
        {
            _movieService = movieService;
            _cartService = cartService;
        }

        //List all movies
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllAsync();
            return View(movies);
        }     
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _movieService.GetByIdAsync(id.Value);
            if (movie == null) return NotFound();

            return View(movie);
        }

        public IActionResult Search()
        {
            return View(); 
        }

        [HttpGet]
        public IActionResult ShowSearchForm()
        {
            return View("Search"); // Renders Views/Movies/Search.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> ShowSearchResult(string searchPhrase)
        {
            ViewBag.SearchPhrase = searchPhrase;

            if (string.IsNullOrWhiteSpace(searchPhrase))
            {
                ViewBag.Message = "Please enter a movie title.";
                ViewBag.ShowResults = false;
                return View("Search");
            }

            var results = await _movieService.SearchAsync(searchPhrase);

            if (results == null || !results.Any())
            {
                ViewBag.Message = $"Movie not found for \"{searchPhrase}\".";
                ViewBag.ShowResults = false;
                return View("Search");
            }

            ViewBag.ShowResults = true;
            return View("Search", results);
        }

        //    //[Authorize]
        //    [HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Add(int movieId)
        //{
        //    _cartService.Add(movieId);
        //    TempData["Message"] = "Movie added to cart!";
        //    return RedirectToAction("Index", "Cart");
        //}


    }

}
