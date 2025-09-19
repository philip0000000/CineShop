using CineShop.DataBase;
using CineShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CineShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly MovieDbContext _db;

        public OrderService(MovieDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Create a new order from cart items for a customer id.
        /// </summary>
        public async Task<int> CreateAsync(int customerId, IReadOnlyList<CartItem> items)
        {
            // Check validation
            if (customerId <= 0)
                throw new ArgumentOutOfRangeException(nameof(customerId));
            if (items == null || items.Count == 0)
                throw new InvalidOperationException("Cart is empty.");

            // Turn cart items into order rows,
            // because the database saves each movie copy as its own row.
            var rows = new List<OrderRow>();
            foreach (var line in items)
            {
                if (line.Quantity <= 0)
                    continue;

                // Add one row for each copy (simple model, 1 row = 1 copy)
                for (int i = 0; i < line.Quantity; i++)
                {
                    rows.Add(new OrderRow
                    {
                        MovieId = line.MovieId,
                        Price = line.Price
                    });
                }
            }

            if (rows.Count == 0)
                throw new InvalidOperationException("No valid rows to save.");

            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow,
                OrderRows = rows
            };

            // Save order to database
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return order.Id;
        }

        /// <summary>
        /// Get order by id with rows and movies.
        /// </summary>
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _db.Orders
                .Include(o => o.OrderRows)
                    .ThenInclude(r => r.Movie)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        /// <summary>
        /// Get all orders for email, newest first.
        /// </summary>
        public async Task<IReadOnlyList<Order>> GetByCustomerEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return Array.Empty<Order>();

            return await _db.Orders
                .Include(o => o.OrderRows)
                    .ThenInclude(r => r.Movie)
                .Include(o => o.Customer)
                .Where(o => o.Customer.EmailAddress == email)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }


        /// <summary>
        /// Get all orders in system, newest first.
        /// </summary>
        public async Task<IReadOnlyList<Order>> GetAllAsync()
        {
            return await _db.Orders
                .Include(o => o.OrderRows)
                    .ThenInclude(r => r.Movie)
                .Include(o => o.Customer)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
