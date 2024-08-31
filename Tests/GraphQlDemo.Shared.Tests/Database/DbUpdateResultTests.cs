using System;

namespace GraphQlDemo.Shared.Tests.Database;

[TestClass]
public class DbUpdateResultTests
{
    [TestMethod]
    public void Constructor_ShouldSetIsSuccessAndEntity()
    {
        // Arrange
        var entity = new TestEntity { Id = 1, Name = "Test" };
        bool isSuccess = true;

        // Act
        var result = new DbUpdateResult<TestEntity>(isSuccess, entity);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(entity, result.Data);
        Assert.IsFalse(result.IsError);
    }

    [TestMethod]
    public void Constructor_ShouldSetIsError_WhenExceptionIsPassed()
    {
        // Arrange
        var exception = new Exception("Test exception");

        // Act
        var result = new DbUpdateResult<TestEntity>(false) { Exception = exception };

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsTrue(result.IsError);
        Assert.AreEqual(exception, result.Exception);
    }

    [TestMethod]
    public void Constructor_ShouldNotSetException_WhenNotProvided()
    {
        // Act
        var result = new DbUpdateResult<TestEntity>(true);

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