namespace GraphQlDemo.Shared.Tests.Database;

[TestClass]
public class DbUpdateResultTests
{
    [TestMethod]
    public void Constructor_ShouldSetIsError_WhenExceptionIsPassed()
    {
        // Arrange
        var exception = new Exception("Test exception");

        // Act
        var result = new DbUpdateResult(false) { Exception = exception };

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsTrue(result.IsError);
        Assert.AreEqual(exception, result.Exception);
    }

    [TestMethod]
    public void Constructor_ShouldNotSetException_WhenNotProvided()
    {
        // Act
        var result = new DbUpdateResult(true);

        // Assert
        Assert.IsFalse(result.IsError);
        Assert.IsNull(result.Exception);
    }

    private class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
