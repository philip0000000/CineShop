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

        [Required]
        [Display(Name = "First Name")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Billing Address")]
        [StringLength(100)]
        public string BillingAddress { get; set; }

        [Required]
        [Display(Name = "Billing City")]
        [StringLength(100)]
        public string BillingCity { get; set; }

        [Required]
        [Display(Name = "Billing Zip")]
        [StringLength(15)]
        public string BillingZip { get; set; }

        [Required]
        [Display(Name = "Delivery Address")]
        [StringLength(100)]
        public string DeliveryAddress { get; set; }

        [Required]
        [Display(Name = "Delivery City")]
        [StringLength(100)]
        public string DeliveryCity { get; set; }

        [Required]
        [Display(Name = "Delivery Zip")]
        [StringLength(15)]
        public string DeliveryZip { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress]
        [StringLength(100)]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15)]
        public string PhoneNo { get; set; }

        // Optional: Navigation property to Orders
        public List<Order> Orders { get; set; } = new List<Order>();




    }
}
