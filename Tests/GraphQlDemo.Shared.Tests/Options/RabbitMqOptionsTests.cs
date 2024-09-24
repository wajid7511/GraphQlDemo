using System.Text.Json;
using GraphQlDemo.Shared.Options;

namespace GraphQlDemo.Shared.Tests.Options;
[TestClass]
public class RabbitMqOptionsTests
{
    [TestMethod]
    public void RabbitMqOptions_DefaultValues_ShouldBeEmptyStrings()
    {
        // Arrange
        var options = new RabbitMqOptions();

        // Act & Assert
        Assert.AreEqual(string.Empty, options.HostName, "HostName should be an empty string by default.");
        Assert.AreEqual(string.Empty, options.UserName, "UserName should be an empty string by default.");
        Assert.AreEqual(string.Empty, options.Password, "Password should be an empty string by default.");
        Assert.AreEqual(string.Empty, options.QueueName, "QueueName should be an empty string by default.");
        Assert.AreEqual(string.Empty, options.ExchangeName, "ExchangeName should be an empty string by default.");
    }

    [TestMethod]
    public void RabbitMqOptions_DeserializeFromJson_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var json = @"
            {
                ""HostName"": ""localhost"",
                ""UserName"": ""guest"",
                ""Password"": ""guest"",
                ""QueueName"": ""myQueue"",
                ""ExchangeName"": ""myExchange""
            }";

        // Act
        var options = JsonSerializer.Deserialize<RabbitMqOptions>(json);

        // Assert
        Assert.IsNotNull(options, "Deserialized object should not be null.");
        Assert.AreEqual("localhost", options.HostName, "HostName was not deserialized correctly.");
        Assert.AreEqual("guest", options.UserName, "UserName was not deserialized correctly.");
        Assert.AreEqual("guest", options.Password, "Password was not deserialized correctly.");
        Assert.AreEqual("myQueue", options.QueueName, "QueueName was not deserialized correctly.");
        Assert.AreEqual("myExchange", options.ExchangeName, "ExchangeName was not deserialized correctly.");
    }
}