using CineShop.DataBase;
using CineShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CineShop.Services
{
    // (Stores the cart in Session)
    public class CartService : ICartService
    {
        private const string CartKey = "cart";
        private readonly IHttpContextAccessor _http;
        private readonly MovieDbContext _context;

        public CartService(IHttpContextAccessor http, MovieDbContext db)
        {
            _http = http;
            _context = db;
        }

        /// <summary>
        /// Adds one copy of the given movie to the cart,
        /// if can not find item in cart.
        /// If found item in cart, increse its quantity.
        /// </summary>
        public void Add(int movieId)
        {
            if (movieId <= 0)
                return;

            var cart = Read();
            var existingItem = cart.FirstOrDefault(x => x.MovieId == movieId);

            if (existingItem == null)
            {
                // Add new CartItem, because it is not in the cart.

                // Fetch only needed fields from database (Id, Title, Price)
                var movie = _context.Movies
                    .Select(m => new { m.Id, m.Title, m.Price })
                    .FirstOrDefault(m => m.Id == movieId);

                if (movie == null) // invalid id
                    return;

                cart.Add(new CartItem
                {
                    MovieId = movie.Id,
                    Title = movie.Title,
                    Price = movie.Price,
                    Quantity = 1
                });
            }
            else
            {
                // Increase quantity if it already exists.
                existingItem.Quantity += 1;
            }

            Write(cart);
        }

        /// <summary>
        /// Removes 1 copy of the given movie from the cart.
        /// If only 1 copy exist of the movie, remove the element from the list.
        /// </summary>
        public void Remove(int movieId)
        {
            if (movieId <= 0)
                return;

            var cart = Read();
            var line = cart.FirstOrDefault(x => x.MovieId == movieId);
            if (line == null) // Did not find any.
                return;

            if (line.Quantity > 1)
                line.Quantity -= 1;
            else
                cart.Remove(line);

            Write(cart);
        }

        /// <summary>
        /// Removes all items from the cart.
        /// </summary>
        public void Clear()
        {
            Write(new List<CartItem>()); // Saving an new empty list to session.
        }

        /// <summary>
        /// Gets all items currently in the cart.
        /// </summary>
        public IReadOnlyList<CartItem> GetItems()
        {
            return Read();
        }

        /// <summary>
        /// Total price of all items currently in the cart.
        /// </summary>
        public decimal GetTotal()
        {
            var list = Read();

            decimal totalPrice = 0;

            foreach (var item in list)
            {
                totalPrice += item.Price * item.Quantity;
            }

            return totalPrice;
        }

        // --- helper methods ---

        /// <summary>
        /// Reads the current cart from session and returns it.
        /// If failure, returns a empty list.
        /// </summary>
        private List<CartItem> Read()
        {
            string? json = _http.HttpContext?.Session.GetString(CartKey);
            if (json == null || json == string.Empty)
                return new List<CartItem>();

            var retult = JsonSerializer.Deserialize<List<CartItem>>(json);
            if (retult == null)
                return new List<CartItem>();

            return retult;
        }

        /// <summary>
        /// Saves current cart data to the session.
        /// </summary>
        private void Write(List<CartItem> items)
        {
            string? json = JsonSerializer.Serialize(items);

            if (_http.HttpContext != null && json != null)
                _http.HttpContext.Session.SetString(CartKey, json);
        }
    }
}
