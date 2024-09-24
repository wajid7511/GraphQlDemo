namespace GraphQlDemo.API.Models;

public class ProductInput
{
    [GraphQLName("productMame")]
    public string ProductName { get; set; } = string.Empty;

    [GraphQLName("productImageUrl")]
    public string ImageUrl { get; set; } = string.Empty;

    [GraphQLName("price")]
    public double Price { get; set; }

    [GraphQLName("groceryId")]
    public int GroceryId { get; set; }
}
