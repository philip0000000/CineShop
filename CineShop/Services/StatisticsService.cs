using CineShop.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CineShop.Models;
using CineShop.DataBase;


namespace CineShop.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly MovieDbContext _context;

        public StatisticsService(MovieDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Movie> GetFiveMostPopularMovies(int topN)
        {
            var topMovieIds = _context.OrderRows
                .GroupBy(or => or.MovieId)
                .OrderByDescending(g => g.Count())
                .Take(topN)
                .Select(g => g.Key)
                .ToList();

            return _context.Movies
                .Where(m => topMovieIds.Contains(m.Id))
                .ToList();
        }



        public IEnumerable<Movie> GetFiveNewestMovies(int topN) =>//Done
            _context.Movies
                .OrderByDescending(l => l.ReleaseYear)
                .Take(topN)
                .ToList();

        public IEnumerable<Movie> GetFiveOldestMovies(int topN) => //Done
            _context.Movies
                .OrderBy(l => l.ReleaseYear)
                .Take(topN)
                .ToList();

        public IEnumerable<Movie> GetFiveCheapestMovies(int topN) =>//Done
            _context.Movies
                .OrderBy(l => l.Price)
                .Take(topN)
                .ToList();

        public Customer GetTopCustomerByTotalSpend()
        {
            return _context.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderRows)
                        .ThenInclude(or => or.Movie)
                .OrderByDescending(c =>
                    c.Orders
                     .SelectMany(o => o.OrderRows)
                     .Sum(or => or.Movie.Price))
                .FirstOrDefault();
        }
    }
}
