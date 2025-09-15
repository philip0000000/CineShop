using CineShop.Models;

namespace CineShop.Services
{
    public interface ICartService
    {
        void Add(int movieId);     // Adds one copy
        void Remove(int movieId);  // Removes one copy
        void Clear();
        IReadOnlyList<CartItem> GetItems();
        decimal GetTotal();
    }
}
