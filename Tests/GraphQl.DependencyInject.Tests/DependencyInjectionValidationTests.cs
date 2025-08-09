using System;
using System.Reflection;
using GraphQl.Abstractions;
using GraphQlDemo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQl.DependencyInject.Tests;

[TestClass]
public class DependencyInjectionValidationTests
{


    [TestMethod]
    public void All_Constructor_Dependencies_Should_Be_Registered()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationManager();
        services.RegisterGraphQlDemoIServicesRegisterModules(configuration);
        services.AddGraphQlDemoServices(configuration);
        // Build service provider for later resolution
        var provider = services.BuildServiceProvider();

        // Find all concrete types registered
        var registeredTypes = services
            .Where(sd => sd.ImplementationType != null)
            .Select(sd => sd.ImplementationType!)
            .Distinct();
        if (!registeredTypes.Any())
        {
            Assert.Fail($"No Record found {registeredTypes}");
        }
        // Act & Assert
        foreach (var type in registeredTypes)
        {
            var constructor = type.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
            if (constructor == null) continue;

            foreach (var parameter in constructor.GetParameters())
            {
                if (!(parameter?.Name?.StartsWith("ILogger") ?? false))
                {
                    continue;
                }
                else
                {
                    var paramType = parameter.ParameterType;
                    var isRegistered = services.Any(s => s.ServiceType == paramType);

                    Assert.IsTrue(isRegistered, $"{type.Name} constructor parameter {paramType.Name} is not registered in DI.");

                }
            }
        }
    }

}
