using CineShop.Models;
using CineShop.Services;
using Microsoft.AspNetCore.Mvc;


namespace CineShop.Controllers
{
    [Route("orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orders;
        private readonly ICustomerService _customers;
        private readonly ICartService _cart;

        public OrdersController(IOrderService orders, ICustomerService customers, ICartService cart)
        {
            _orders = orders;
            _customers = customers;
            _cart = cart;
        }

        /// <summary>
        /// Shows all orders for customer email.
        /// </summary>
        // GET /orders/customer/{email}
        [HttpGet("customer/{email}")]
        public async Task<IActionResult> CustomerOrders(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return NotFound();

            // Load all orders from the email. Ok, if it is 0 orders.
            var list = await _orders.GetByCustomerEmailAsync(email);
            ViewBag.Email = email;
            ViewBag.TotalCount = list.Count;

            // Show orders. If we had 0 orders, show empty list. (instead of error)
            return View(list);
        }

        /// <summary>
        /// Admin view: all orders, newest first.
        /// </summary>
        // GET /orders/admin
        [HttpGet("admin")]
        public async Task<IActionResult> AdminOrders()
        {
            var list = await _orders.GetAllAsync();
            return View(list);
        }

        /// <summary>
        /// Handle checkout form post: create order from cart.
        /// If email exists, reuse customer. Otherwise create new.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(Customer input)
        {
            if (input == null)
                return BadRequest();

            // Get all cart items. Stop if cart is empty.
            var items = _cart.GetItems();
            if (items == null || items.Count == 0)
            {
                TempData["Message"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            int customerId;

            // Try existing customer by email
            if (!string.IsNullOrWhiteSpace(input.EmailAddress) &&
                await _customers.EmailExistsAsync(input.EmailAddress))
            {
                var existing = await _customers.GetByEmailWithOrdersAsync(input.EmailAddress);
                if (existing == null) return NotFound();
                customerId = existing.Id;
            }
            else
            {
                // If no customer with this email, we must create a new one
                // First check that the form data is valid before saving
                if (ModelState.IsValid == false)
                {
                    TempData["Message"] = "Please fix the form and try again.";
                    return RedirectToAction("Checkout", "Cart");
                }

                customerId = await _customers.CreateAsync(input);
            }

            // Create order in database from cart items
            var orderId = await _orders.CreateAsync(customerId, items);

            _cart.Clear(); // Empty cart after order is done

            // Go to success page showing order info
            return RedirectToAction(nameof(Success), new { id = orderId });
        }

        /// <summary>
        /// Success page after order creation.
        /// </summary>
        // GET /orders/success
        [HttpGet("success")]
        public async Task<IActionResult> Success(int id)
        {
            var order = await _orders.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}
