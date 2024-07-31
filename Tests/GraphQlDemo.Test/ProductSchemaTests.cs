using GraphQlDemo.API.Models;

namespace GraphQlDemo.Test;

[TestClass]
public class ProductSchemaTests : BaseGraphqlSchemaTests
{
    [TestInitialize]
    public void Setup() { }

    [TestCleanup]
    public void Cleanup()
    {
        //
    }

    public override object GetObj()
    {
        return new ProductSchema();
    }

    public override void AssertGraphQlName(Dictionary<string, string> graphQlNameDictionary)
    {
        Assert.AreEqual("id",graphQlNameDictionary[nameof(ProductSchema.Id)]);
        Assert.AreEqual("name",graphQlNameDictionary[nameof(ProductSchema.Name)]);
        Assert.AreEqual("productImageUrl",graphQlNameDictionary[nameof(ProductSchema.ProductImageUrl)]);
        Assert.AreEqual("groceryId",graphQlNameDictionary[nameof(ProductSchema.GroceryId)]);
        Assert.AreEqual("createdOn",graphQlNameDictionary[nameof(ProductSchema.CreatedOn)]);
        Assert.AreEqual("lastUpdateTime",graphQlNameDictionary[nameof(ProductSchema.LastUpdateTime)]);
        Assert.AreEqual("grocery",graphQlNameDictionary[nameof(ProductSchema.Grocery)]);
    }
}
