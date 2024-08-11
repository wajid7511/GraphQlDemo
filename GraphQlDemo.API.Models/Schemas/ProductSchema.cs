namespace GraphQlDemo.API.Models;

public class ProductSchema
{
    [GraphQLName("id")]
    public int Id { get; set; }

    [GraphQLName("name")]
    public string Name { get; set; } = string.Empty;

    [GraphQLName("productImageUrl")]
    public string ProductImageUrl { get; set; } = string.Empty;

    [GraphQLName("price")]
    public double Price { get; set; }

    [GraphQLName("groceryId")]
    public int GroceryId { get; set; }

    [GraphQLName("createdOn")]
    public DateTimeOffset CreatedOn { get; set; }

    [GraphQLName("lastUpdateTime")]
    public DateTimeOffset? LastUpdateTime { get; set; }

    [GraphQLName("grocery")]
    public GrocerySchema? Grocery { get; set; }
}
