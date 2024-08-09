namespace GraphQlDemo.API.Models;

public class ProductInput
{ 

    [GraphQLName("productMame")]
    public string ProductName { get; set; } = string.Empty;

    [GraphQLName("productImageUrl")]
    public string ProductImageUrl { get; set; } = string.Empty;

    [GraphQLName("groceryId")]
    public int GroceryId { get; set; }  
}
