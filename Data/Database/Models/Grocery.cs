using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQl.Database.Models
{
    [Table("Groceries")]
    public class Grocery
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name should be between 3-50 characters")]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? LastUpdateTime { get; set; }
    }
}

