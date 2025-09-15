using CineShop.Models;
using CineShop.Services;
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

        //List of Movies
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllAsync();
            return View(movies);
        }

       //Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _movieService.GetByIdAsync(id.Value);
            if (movie == null) return NotFound();

            return View(movie);
        }

      
        [HttpGet]
        public IActionResult AddNewMovie()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewMovie(Movie movie)
        {
            if (!ModelState.IsValid) return View(movie);

            await _movieService.AddAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _movieService.GetByIdAsync(id.Value);
            if (movie == null) return NotFound();

            return View(movie);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.Id) return NotFound();
            if (!ModelState.IsValid) return View(movie);

            try
            {
                await _movieService.UpdateAsync(movie);
            }
            catch
            {
                if (!await _movieService.ExistsAsync(movie.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _movieService.GetByIdAsync(id.Value);
            if (movie == null) return NotFound();

            return View(movie);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movieService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //TO DO
        [HttpGet]
        public IActionResult ShowSearchForm()
        {
            return View();
        }

        // POST: /Movies/ShowSearchResult
        [HttpPost]
        public async Task<IActionResult> ShowSearchResult(string searchPhrase)
        {
            if (string.IsNullOrWhiteSpace(searchPhrase))
                return View("Index", new List<Movie>());

            var results = await _movieService.SearchAsync(searchPhrase);
            return View("Index", results);
        }

        // POST: /Movies/Add (adds to cart)//Done
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
