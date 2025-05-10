using GraphQl.Abstractions;
using GraphQl.Common;
using GraphQl.DependencyInject.Tests.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQl.DependencyInject.Tests;

[TestClass]
public class CommonRegisterServicesTests : ServiceCollectionAssert
{
    [TestMethod]
    public void RegisterServices_Should_Register_IDateTimeProvider_As_Singleton()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationManager(); // You can mock this if needed
        var module = new CommonRegisterServices();

        // Act
        module.RegisterServices(services, configuration);

        // Assert
        HasSingleton<IDateTimeProvider, DefaultDateTimeProvider>(services);
    }
}