using CineShop.Models;

namespace CineShop.Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// Check if an email already exists.
        /// </summary>
        Task<bool> EmailExistsAsync(string email);

        /// <summary>
        /// Create a new customer. Returns the new id.
        /// </summary>
        Task<int> CreateAsync(Customer customer);

        /// <summary>
        /// Get a customer by id.
        /// </summary>
        Task<Customer?> GetByIdAsync(int id);

        /// <summary>
        /// Get a customer by id, include orders.
        /// </summary>
        Task<Customer?> GetByIdWithOrdersAsync(int id);

        /// <summary>
        /// Get a customer by email, include orders.
        /// </summary>
        Task<Customer?> GetByEmailWithOrdersAsync(string email);

        /// <summary>
        /// Update a customer by id. Returns true if updated.
        /// </summary>
        Task<bool> UpdateAsync(int id, Customer updated);
    }
}
