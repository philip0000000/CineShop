using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineShop.Models
{
    [Table("Movie")]
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Genre")]
        [StringLength(100, ErrorMessage = "Genre cannot exceed 100 characters.")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Director name is required.")]
        [StringLength(100, ErrorMessage = "Director name cannot exceed 100 characters.")]
        [Display(Name = "Director")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Release year is required.")]
        [Display(Name = "Release Year")]
        [Range(1900, 2100, ErrorMessage = "Release year must be between 1900 and 2100.")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Display(Name = "Price")]
        [Range(99.00, 599.00, ErrorMessage = "Price must be between 99.00 and 599.00.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        [StringLength(300, ErrorMessage = "Image URL cannot exceed 300 characters.")]
        public string? Image { get; set; }

        // Navigation property for related order rows
        public virtual ICollection<OrderRow> OrderRows { get; set; } = new List<OrderRow>();

        public Movie()
        {

        }

    }
}
