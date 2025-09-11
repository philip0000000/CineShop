using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineShop.Models
{
    [Table("Movie")]
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Director")]
        public string Director { get; set; }

        [Required]
        [Display(Name = "Release Year")]
        [DataType(DataType.Date)]
        public DateTime ReleaseYear { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string? Image { get; set; }

        // Navigation property for related order rows
        public virtual ICollection<OrderRow> OrderRows { get; set; } = new List<OrderRow>();

    }
}
