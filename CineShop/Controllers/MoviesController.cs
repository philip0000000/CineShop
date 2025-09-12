using CineShop.DataBase;
using CineShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CineShop.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieDbContext _context; //Added Polina to test seed data

        public MoviesController(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() //Done
        {

            return View(await _context.Movies.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)//Done
        { 
           if(id == null)
           {
                return NotFound();
           }
            var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.Id == id);

            if(movie == null)
            {
                return NotFound();
            }

           return View(movie);
        }


        //[Authorize] 

        [HttpGet]
        public IActionResult AddNewMovie()//Done
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewMovie([Bind("Id,Title,Genre,Director,ReleaseYear,Price,Image")] Movie movie)
        {
            if (ModelState.IsValid)  
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);

        }

        //[Authorize]

        [HttpGet]
        public async Task<IActionResult> Edit(int? id) // Done
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FindAsync(id);
            
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Director,ReleaseYear,Price,Image")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);

        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

        //[Authorize]
        public async Task<IActionResult> Delete(int? id) // Todo
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int? id) // Todo
        {

            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            
        }




        [HttpGet]
        public IActionResult ShowSearchForm()//Done
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ShowSearchResult(string searchPhrase)
        {
            if (string.IsNullOrWhiteSpace(searchPhrase))
            {
                return View("Index", new List<Movie>());
            }

            var normalizedPhrase = searchPhrase.Trim().ToLower();

            var results = await _context.Movies
                .Where(m => m.Title.ToLower().Contains(normalizedPhrase))
                .ToListAsync();

            return View("Index", results);
        }
        

        public IActionResult AddToCart()//To Do
        {
            return View();
        }
    }
    
}
