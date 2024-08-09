using GraphQlDemo.API.Models;
using GraphQlDemo.Test;

[TestClass]
public class ProductInputTests : BaseGraphqlSchemaTests
{  
    public override object GetObj()
    {
        return new ProductInput();
    }

    public override void AssertGraphQlName(Dictionary<string, string> graphQlNameDictionary)
    {
        Assert.AreEqual("productName", graphQlNameDictionary[nameof(ProductInput.ProductName)]);
        Assert.AreEqual("productImageUrl", graphQlNameDictionary[nameof(ProductInput.ProductImageUrl)]);
        Assert.AreEqual("groceryId", graphQlNameDictionary[nameof(ProductInput.GroceryId)]); 
    }
}