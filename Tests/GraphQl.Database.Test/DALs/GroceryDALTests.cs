using GraphQl.Abstractions;
using GraphQl.Database.DAL;
using GraphQl.Database.Models;
using Moq;

namespace GraphQl.Database.Test.DALs;

[TestClass]
public class GroceryDALTests
{
    private GroceryDAL _groceryDAL = null!;
    private GraphQlDatabaseContext _context = null!;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider = new();

    [TestInitialize]
    public void Setup()
    {
        _dateTimeProvider.Setup(s => s.UtcNow).Returns(DateTime.UtcNow);
        _context = MockFactory.GraphQlDatabaseContext(_dateTimeProvider);

        _groceryDAL = new GroceryDAL(_context);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
    }

    [TestMethod]
    public void AddGrocery_Async_ShouldThrowArgumentNullException_WhenGroceryIsNull()
    {
        // Act & Assert
        _ = Assert.ThrowsException<ArgumentNullException>(
            () => _groceryDAL.AddGrocery_Async(null as Grocery).GetAwaiter().GetResult()
        );
    }

    [TestMethod]
    public async Task AddGrocery_Async_ShouldReturnSuccess_WhenGroceryIsAdded()
    {
        // Arrange
        var grocery = new Grocery { Id = 1, Name = "Apple" };

        // Act
        var result = await _groceryDAL.AddGrocery_Async(grocery);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(grocery, result.Entity);
        Assert.IsNull(result.Exception);
    }

    [TestMethod]
    public async Task AddGrocery_Async_ShouldReturnError_WhenSaveChangesFails()
    {
        // Arrange
        var grocery = new Grocery { Id = 1, Name = "Apple", };

        // Simulate failure by disposing of the context before SaveChangesAsync
        _context.Dispose();

        // Act
        var result = await _groceryDAL.AddGrocery_Async(grocery);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsNull(result.Entity);
        Assert.IsNotNull(result.Exception);
    }
}
