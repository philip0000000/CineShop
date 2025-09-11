using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineShop.Models
{
    [Table("Order")]
    public class Order
    {
       
            [Key]
            public int Id { get; set; }

            [Required]
            [Display(Name = "Order Date")]
            [DataType(DataType.Date)]
            public DateTime OrderDate { get; set; }

            [Required]       
            public int CustomerId { get; set; }
            public virtual Customer Customer { get; set; } // Foreign key to Customer

            [Display(Name = "Order Rows")]
            public virtual ICollection<OrderRow> OrderRows { get; set; } = new List<OrderRow>();
        
    }
}
