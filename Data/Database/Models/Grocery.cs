using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    [Table("Groceries")]
    public class Grocery
    {
        public Grocery()
        {
            Products = Array.Empty<Product>();
            Name = string.Empty;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name should be between 3-50 characters")]
        public string Name { get; set; }

        [Required]
        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset? LastUpdateTime { get; set; }
        #region
        public IEnumerable<Product> Products { get; set; }
        #endregion
    }
}

