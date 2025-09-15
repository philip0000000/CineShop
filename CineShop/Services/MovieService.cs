using CineShop.DataBase;
using CineShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineShop.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _context;

        public MovieService(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllAsync() =>
            await _context.Movies.ToListAsync();

        public async Task<Movie> GetByIdAsync(int id) =>
            await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

        public async Task AddAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id) =>
            await _context.Movies.AnyAsync(m => m.Id == id);

        public async Task<List<Movie>> SearchAsync(string searchPhrase)
        {
            var normalized = searchPhrase.Trim().ToLower();
            return await _context.Movies
                .Where(m => m.Title.ToLower().Contains(normalized))
                .ToListAsync();
        }
    }
}
