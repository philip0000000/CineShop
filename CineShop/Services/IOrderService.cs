using CineShop.Models;
using CineShop.ViewModels;

namespace CineShop.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Create order for customer id from cart items.
        /// Returns new order id.
        /// </summary>
        Task<int> CreateAsync(int customerId, IReadOnlyList<CartItem> items);

        /// <summary>
        /// Get one order by id with rows and movies.
        /// </summary>
        Task<Order?> GetByIdAsync(int id);

        /// <summary>
        /// Get all orders for a customer email. Newest first.
        /// </summary>
        Task<IReadOnlyList<Order>> GetByCustomerEmailAsync(string email);

        /// <summary>
        /// Get all orders in system. Newest first.
        /// </summary>
        Task<IReadOnlyList<Order>> GetAllAsync();
    }
}
