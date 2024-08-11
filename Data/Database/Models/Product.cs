using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GraphQl.Database.Models
{
    [Table("Products")]
    public class Product : DbBaseModel
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(
            50,
            MinimumLength = 3,
            ErrorMessage = "Name should be between 3-50 characters"
        )]
        public string Name { get; set; } = string.Empty;

        [StringLength(
            100,
            MinimumLength = 3,
            ErrorMessage = "ImageUrl should be between 3-50 characters"
        )]
        public string ImageUrl { get; set; } = string.Empty;

        [Column("PriceAmount", TypeName = "decimal(9,2)")]
        public int Price { get; set; }

        [Required]
        [ForeignKey("Grocery")]
        public int GroceryId { get; set; }
        #endregion

        #region Navigations
        public Grocery? Grocery { get; set; }
        #endregion
    }
}
