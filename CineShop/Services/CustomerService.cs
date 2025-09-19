using CineShop.DataBase;
using CineShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CineShop.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly MovieDbContext _db;

        public CustomerService(MovieDbContext db)
        {
            _db = db;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var cleanedEmail = email.Trim().ToLower();

            // Look in the database if any customer has this email.
            bool exists = await _db.Customers
                .AnyAsync(c => c.EmailAddress.ToLower() == cleanedEmail);

            return exists;
        }

        public async Task<int> CreateAsync(Customer customer)
        {
            if (customer == null)
                return 0;

            _db.Customers.Add(customer);
            await _db.SaveChangesAsync(); // EF sets Id after save
            return customer.Id;
        }

        public Task<Customer?> GetByIdAsync(int id)
        {
            // simple read by id
            return _db.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Customer?> GetByIdWithOrdersAsync(int id)
        {
            // include orders and order rows
            return _db.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderRows)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer?> GetByEmailWithOrdersAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var cleanedEmail = email.Trim().ToLower();

            // Find the customer with this email, include their orders and order rows.
            var customer = await _db.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderRows)
                .FirstOrDefaultAsync(c => c.EmailAddress.ToLower() == cleanedEmail);

            return customer;
        }

       

        public async Task<bool> UpdateAsync(int id, Customer updated)
        {
            // Find customer id in database
            var existing = await _db.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (existing == null)
                return false; // Could not find customer in database, return false.

            // Update the fields
            existing.FirstName = updated.FirstName;
            existing.LastName = updated.LastName;
            existing.BillingAddress = updated.BillingAddress;
            existing.BillingCity = updated.BillingCity;
            existing.BillingZip = updated.BillingZip;
            existing.DeliveryAddress = updated.DeliveryAddress;
            existing.DeliveryCity = updated.DeliveryCity;
            existing.DeliveryZip = updated.DeliveryZip;
            existing.EmailAddress = updated.EmailAddress;
            existing.PhoneNo = updated.PhoneNo;

            await _db.SaveChangesAsync();
            return true;
        }
    }
}
