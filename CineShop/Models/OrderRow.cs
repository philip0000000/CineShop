using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineShop.Models
{
    [Table("Order Row")]
    public class OrderRow
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Movie ID")]
        public int MovieId { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        
        public virtual Order Order { get; set; }        
        public virtual Movie Movie { get; set; }
    }
}
