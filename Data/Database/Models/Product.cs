using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    [Table("Products")]
    public class Product
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name should be between 3-50 characters")]
        public string Name { get; set; } = string.Empty;


        [StringLength(100, MinimumLength = 3, ErrorMessage = "ImageUrl should be between 3-50 characters")]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        [ForeignKey("Grocery")]
        public int GroceryId { get; set; }

        [Required]
        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset? LastUpdateTime { get; set; }
        #endregion

        #region Navigations
        public Grocery? Grocery { get; set; }
        #endregion
    }
}

