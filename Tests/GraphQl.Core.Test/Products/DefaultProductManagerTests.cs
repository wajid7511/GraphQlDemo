using AutoMapper;
using GraphQl.Abstractions;
using GraphQl.Database;
using GraphQl.Database.DAL;
using GraphQl.Database.Models;
using GraphQlDemo.API.Models;
using Moq;

namespace GraphQl.Core.Test.Products;

[TestClass]
public class DefaultProductManagerTests
{
    private readonly Mock<ProductDAL> _mockProductDAL = new(MockFactory.GraphQlDatabaseContext());
    private readonly Mock<IMapper> _mockMapper = new();
    private IProductManager _productManager = null!;

    [TestInitialize]
    public void Setup()
    {
        _productManager = new DefaultProductManager(_mockProductDAL.Object, _mockMapper.Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task AddProductAsync_ShouldThrowArgumentNullException_WhenProductInputIsNull()
    {
        // Act
        await _productManager.AddProductAsync(null);
    }

    [TestMethod]
    public async Task AddProductAsync_ShouldReturnProductId_WhenProductIsAddedSuccessfully()
    {
        // Arrange
        var productInput = new ProductInput();
        var product = new Product { Id = 1 };
        var dbAddResult = new DbAddResult<Product>(true, product);

        _mockMapper.Setup(m => m.Map<Product>(productInput)).Returns(product);
        _mockProductDAL.Setup(dal => dal.AddProduct_Async(product)).ReturnsAsync(dbAddResult);

        // Act
        var result = await _productManager.AddProductAsync(productInput);

        // Assert
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public async Task AddProductAsync_ShouldReturnZero_WhenProductDALThrowsException()
    {
        // Arrange
        var productInput = new ProductInput();
        var product = new Product();
        var dbAddResult = new DbAddResult<Product>(true, null);

        _mockMapper.Setup(m => m.Map<Product>(productInput)).Returns(product);
        _mockProductDAL.Setup(dal => dal.AddProduct_Async(product)).ReturnsAsync(dbAddResult);

        // Act
        var result = await _productManager.AddProductAsync(productInput);

        // Assert
        Assert.AreEqual(0, result);
    }
}
