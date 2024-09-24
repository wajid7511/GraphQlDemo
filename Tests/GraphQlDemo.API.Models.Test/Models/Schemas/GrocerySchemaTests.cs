namespace GraphQlDemo.API.Models.Test.Schemas;

[TestClass]
public class GrocerySchemaTests : BaseGraphqlSchemaTests
{
    public override object GetObj()
    {
        return new GrocerySchema();
    }

    public override void AssertGraphQlName(Dictionary<string, string> graphQlNameDictionary)
    {
        Assert.AreEqual("id", graphQlNameDictionary[nameof(GrocerySchema.Id)]);
        Assert.AreEqual("name", graphQlNameDictionary[nameof(GrocerySchema.Name)]);
        Assert.AreEqual("createdOn", graphQlNameDictionary[nameof(GrocerySchema.CreatedOn)]);
        Assert.AreEqual(
            "lastUpdateTime",
            graphQlNameDictionary[nameof(GrocerySchema.LastUpdateTime)]
        );
    }
}
