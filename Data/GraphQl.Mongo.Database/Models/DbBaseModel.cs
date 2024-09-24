using System;
using System.ComponentModel.DataAnnotations;

namespace GraphQl.Mongo.Database.Models;

public class DbBaseModel
{
    [Required]
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? LastUpdateTime { get; set; }

    protected DbBaseModel()
    {
        // The actual initialization is handled elsewhere
    }
}
