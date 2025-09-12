namespace CineShop.Models
{
    /// <summary>
    /// Item stored in the users cart (kept in session)
    /// </summary>
    public class CartItem
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
