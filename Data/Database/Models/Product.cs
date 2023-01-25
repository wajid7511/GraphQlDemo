using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Azure;

namespace Database.Models
{
    [Table("Products")]
    public class Product
    {
        public Product()
        {
            Name = string.Empty;
            ImageUrl = string.Empty;
        }
        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name should be between 3-50 characters")]
        public string Name { get; set; }


        [StringLength(100, MinimumLength = 3, ErrorMessage = "ImageUrl should be between 3-50 characters")]
        public string ImageUrl { get; set; }
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

