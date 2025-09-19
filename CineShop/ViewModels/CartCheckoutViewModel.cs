namespace CineShop.ViewModels
{
    public class CartCheckoutViewModel
    {
        public string? ExistingEmail { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? BillingAddress { get; set; }
        public string? BillingZip { get; set; }
        public string? BillingCity { get; set; }

        public string? DeliveryAddress { get; set; }
        public string? DeliveryZip { get; set; }
        public string? DeliveryCity { get; set; }

        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
    }
}
