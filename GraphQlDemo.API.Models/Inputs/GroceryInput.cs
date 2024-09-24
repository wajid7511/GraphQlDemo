namespace GraphQlDemo.API.Models;

public class GroceryInput
{
    [GraphQLName("name")]
    public string Name { get; set; } = string.Empty;
}
