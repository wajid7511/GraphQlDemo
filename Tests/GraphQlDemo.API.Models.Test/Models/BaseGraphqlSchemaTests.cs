using System.Reflection;
using HotChocolate;

namespace GraphQlDemo.API.Models.Test;

public abstract class BaseGraphqlSchemaTests
{
    [TestMethod]
    public void VerifyGraphQlName()
    {
        var obj = GetObj();
        Dictionary<string, string> graphQlNameDictionary = new();
        Type type = obj.GetType();
        PropertyInfo[] props = type.GetProperties();  
        foreach (PropertyInfo prp in props)
        {
            var propertyAttributes = prp.GetCustomAttribute<GraphQLNameAttribute>();
            Assert.IsNotNull(propertyAttributes);
            graphQlNameDictionary.Add(prp.Name, propertyAttributes.Name);
        }
        AssertGraphQlName(graphQlNameDictionary);
    }

    public abstract void AssertGraphQlName(Dictionary<string, string> graphQlNameDictionary);
    public abstract object GetObj();
}
