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

        // 🟡 Public: List all movies
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllAsync();
            return View(movies);
        }

        // 🟡 Public: View movie details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _movieService.GetByIdAsync(id.Value);
            if (movie == null) return NotFound();

            return View(movie);
        }

        // 🟡 Public: Show search form
        [HttpGet]
        public IActionResult ShowSearchForm()
        {
            return View();
        }

        // 🟡 Public: Handle search result
        [HttpPost]
        public async Task<IActionResult> ShowSearchResult(string searchPhrase)
        {
            if (string.IsNullOrWhiteSpace(searchPhrase))
                return View("Index", new List<Movie>());

            var results = await _movieService.SearchAsync(searchPhrase);
            return View("Index", results);
        }

        // 🟡 Public (Authenticated): Add movie to cart
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int movieId)
        {
            _cartService.Add(movieId);
            TempData["Message"] = "Movie added to cart!";
            return RedirectToAction("Index", "Cart");
        }
    }
}
