namespace GraphQlDemo.API.Models.Test.Models.Input;

[TestClass]
public class GroceryInputTests : BaseGraphqlSchemaTests
{
    public override object GetObj()
    {
        return new GroceryInput();
    }

    public override void AssertGraphQlName(Dictionary<string, string> graphQlNameDictionary)
    {
        Assert.AreEqual("name", graphQlNameDictionary[nameof(GroceryInput.Name)]);
    }
}
