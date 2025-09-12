using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineShop.Models
{
    [Table("Customer")]
    public class Customer
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Billing address is required.")]
        [Display(Name = "Billing Address")]
        [StringLength(100, ErrorMessage = "Billing address cannot exceed 100 characters.")]
        public string BillingAddress { get; set; }

        [Required(ErrorMessage = "Billing city is required.")]
        [Display(Name = "Billing City")]
        [StringLength(100, ErrorMessage = "Billing city cannot exceed 100 characters.")]
        public string BillingCity { get; set; }

        [Required(ErrorMessage = "Billing ZIP is required.")]
        [Display(Name = "Billing Zip")]
        [StringLength( 20, ErrorMessage = "Billing ZIP must be between 4 and 15 digits.")]
        public string BillingZip { get; set; }

        [Required(ErrorMessage = "Delivery address is required.")]
        [Display(Name = "Delivery Address")]
        [StringLength(100, ErrorMessage = "Delivery address cannot exceed 100 characters.")]
        public string DeliveryAddress { get; set; }

        [Required(ErrorMessage = "Delivery city is required.")]
        [Display(Name = "Delivery City")]
        [StringLength(100, ErrorMessage = "Delivery city cannot exceed 100 characters.")]
        public string DeliveryCity { get; set; }

        [Required(ErrorMessage = "Delivery ZIP is required.")]
        [Display(Name = "Delivery Zip")]
        [StringLength(20, ErrorMessage = "Delivery ZIP must be between 4 and 15 digits.")]
        public string DeliveryZip { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email address cannot exceed 100 characters.")]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string PhoneNo { get; set; }

        // Optional: Navigation property to Orders
        public List<Order> Orders { get; set; } = new List<Order>();




    }
}
