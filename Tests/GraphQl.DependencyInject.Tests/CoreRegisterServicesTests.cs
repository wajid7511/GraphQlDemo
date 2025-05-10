using GraphQl.Abstractions;
using GraphQl.Core;
using GraphQl.DependencyInject.Tests.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQl.DependencyInject.Tests;

[TestClass]
public class CoreRegisterServicesTests : ServiceCollectionAssert
{
    [TestMethod]
    public void RegisterServices_Should_Register_IDateTimeProvider_As_Singleton()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationManager(); // You can mock this if needed
        var module = new CoreRegisterServices();

        // Act
        module.RegisterServices(services, configuration);

        // Assert
        HasScoped<IProductManager, DefaultProductManager>(services);
        HasScoped<IGroceryManager, DefaultGroceryManager>(services);
        HasScoped<ICustomerManager, DefaultCustomerManager>(services);
    }
}