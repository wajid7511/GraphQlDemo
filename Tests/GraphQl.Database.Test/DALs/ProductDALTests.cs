using GraphQl.Abstractions;
using GraphQl.Database.DAL;
using GraphQl.Database.Models;
using Moq;

namespace GraphQl.Database.Test.DALs;

[TestClass]
public class ProductDALTests
{
    private ProductDAL _ProductDAL = null!;
    private GraphQlDatabaseContext _context = null!;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider = new();

    [TestInitialize]
    public void Setup()
    {
        _dateTimeProvider.Setup(s => s.UtcNow).Returns(DateTime.UtcNow);
        _context = MockFactory.GraphQlDatabaseContext(_dateTimeProvider);

        _ProductDAL = new ProductDAL(_context);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
    }

    [TestMethod]
    public void AddProduct_Async_ShouldThrowArgumentNullException_WhenProductIsNull()
    {
        // Act & Assert
        _ = Assert.ThrowsException<ArgumentNullException>(
            () => _ProductDAL.AddProduct_Async(null as Product).GetAwaiter().GetResult()
        );
    }

    [TestMethod]
    public async Task AddProduct_Async_ShouldReturnSuccess_WhenProductIsAdded()
    {
        // Arrange
        var Product = new Product { Id = 1, Name = "Apple" };

        // Act
        var result = await _ProductDAL.AddProduct_Async(Product);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(Product, result.Entity);
        Assert.IsNull(result.Exception);
    }

    [TestMethod]
    public async Task AddProduct_Async_ShouldReturnError_WhenSaveChangesFails()
    {
        // Arrange
        var Product = new Product { Id = 1, Name = "Apple", };

        // Simulate failure by disposing of the context before SaveChangesAsync
        _context.Dispose();

        // Act
        var result = await _ProductDAL.AddProduct_Async(Product);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsNull(result.Entity);
        Assert.IsNotNull(result.Exception);
    }
}
