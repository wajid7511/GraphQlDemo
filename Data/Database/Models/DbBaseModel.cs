using System;
using System.ComponentModel.DataAnnotations;

namespace GraphQl.Database.Models;

public class DbBaseModel
{
    [Required]
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? LastUpdateTime { get; set; }
}
