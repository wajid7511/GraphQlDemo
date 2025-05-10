using GraphQl.Core;
using GraphQl.Database.DAL;
using GraphQl.DependencyInject.Tests.Base;

namespace GraphQl.DependencyInject.Tests;

[TestClass]
public class DatabaseRegisterServicesTests : ServiceCollectionAssert
{
    [TestMethod]
    public void RegisterServices_Should_Register_Services_Into_DI()
    {
        // Arrange 
        var module = new DatabaseRegisterServices();

        // Act
        module.RegisterServices(Services, Configuration);

        // Assert
        HasScoped<ProductDAL>();
        HasScoped<GroceryDAL>();
    }
}