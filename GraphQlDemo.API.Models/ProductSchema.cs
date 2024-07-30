namespace GraphQlDemo.API.Models;

public class ProductSchema
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int GroceryId { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? LastUpdateTime { get; set; }
    public GrocerySchema? Grocery { get; set; }
}

