using CineShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineShop.Services
{
    public interface IAdminService
    {

        // --- Movie Management ---
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie?> GetMovieByIdAsync(int id);
        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(int id);

        // --- Customer Management ---
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);

        // --- Order Management ---
        Task<IEnumerable<Order>> GetAllOrdersAsync(); // Sorted newest to oldest
        Task<Order?> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
