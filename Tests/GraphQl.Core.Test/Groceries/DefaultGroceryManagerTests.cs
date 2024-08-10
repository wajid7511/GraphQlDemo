using AutoMapper;
using GraphQl.Database;
using GraphQl.Database.DAL;
using GraphQl.Database.Models;
using GraphQlDemo.API.Models;
using Moq;

namespace GraphQl.Core.Test.Groceries;

[TestClass]
public class DefaultGroceryManagerTests
{
    private readonly Mock<GroceryDAL> _mockGroceryDAL = new(MockFactory.GraphQlDatabaseContext());
    private readonly Mock<IMapper> _mockMapper = new();
    private IGroceryManager _groceryManager = null!;

    [TestInitialize]
    public void Setup()
    {
        _groceryManager = new DefaultGroceryManager(_mockGroceryDAL.Object, _mockMapper.Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task AddGroceryAsync_ShouldThrowArgumentNullException_WhenGroceryInputIsNull()
    {
        // Act
        await _groceryManager.AddGroceryAsync(null);
    }

    [TestMethod]
    public async Task AddGroceryAsync_ShouldReturnGroceryId_WhenGroceryIsAddedSuccessfully()
    {
        // Arrange
        var groceryInput = new GroceryInput();
        var grocery = new Grocery { Id = 1 };
        var dbAddResult = new DbAddResult<Grocery>(true, grocery);

        _mockMapper.Setup(m => m.Map<Grocery>(groceryInput)).Returns(grocery);
        _mockGroceryDAL.Setup(dal => dal.AddGrocery_Async(grocery)).ReturnsAsync(dbAddResult);

        // Act
        var result = await _groceryManager.AddGroceryAsync(groceryInput);

        // Assert
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public async Task AddGroceryAsync_ShouldReturnZero_WhenGroceryDALThrowsException()
    {
        // Arrange
        var groceryInput = new GroceryInput();
        var grocery = new Grocery();
        var dbAddResult = new DbAddResult<Grocery>(true, null);

        _mockMapper.Setup(m => m.Map<Grocery>(groceryInput)).Returns(grocery);
        _mockGroceryDAL.Setup(dal => dal.AddGrocery_Async(grocery)).ReturnsAsync(dbAddResult);

        // Act
        var result = await _groceryManager.AddGroceryAsync(groceryInput);

        // Assert
        Assert.AreEqual(0, result);
    }
}
