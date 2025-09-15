using CineShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineShop.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<Movie>> SearchAsync(string searchPhrase);
    }
}
