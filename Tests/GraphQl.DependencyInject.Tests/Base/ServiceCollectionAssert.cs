using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GraphQl.DependencyInject.Tests.Base;

public abstract class ServiceCollectionAssert
{
    protected ServiceCollection Services { get; private set; }
    protected ConfigurationManager Configuration { get; private set; }
    public ServiceCollectionAssert()
    {
        Services = new ServiceCollection();
        Configuration = new ConfigurationManager(); // You can mock this if neededs
    }
    public void HasOptionsConfigured<TOptions>() where TOptions : class
    {
        var descriptor = Services.FirstOrDefault(d =>
            d.ServiceType == typeof(IConfigureOptions<TOptions>));

        Assert.IsNotNull(descriptor, $"Options of type {typeof(TOptions).Name} were not registered.");
    }
    public void HasScoped<TImplementation>()
    {
        var descriptor = Services.FirstOrDefault(d => d.ServiceType == typeof(TImplementation));
        Assert.IsNotNull(descriptor, $"{typeof(TImplementation).Name} was not registered.");
        Assert.AreEqual(ServiceLifetime.Scoped, descriptor.Lifetime, $"Expected singleton for {typeof(TImplementation).Name}.");
    }
    public void HasSingleton<TService>()
    {
        var descriptor = Services.FirstOrDefault(d => d.ServiceType == typeof(TService));
        Assert.IsNotNull(descriptor, $"{typeof(TService).Name} was not registered.");
        Assert.AreEqual(ServiceLifetime.Singleton, descriptor.Lifetime, $"Expected singleton for {typeof(TService).Name}.");
    }
    public void HasSingleton<TService, TImplementation>()
    {
        var descriptor = Services.FirstOrDefault(d => d.ServiceType == typeof(TService));
        Assert.IsNotNull(descriptor, $"{typeof(TService).Name} was not registered.");
        Assert.AreEqual(ServiceLifetime.Singleton, descriptor.Lifetime, $"Expected singleton for {typeof(TService).Name}.");
        Assert.AreEqual(typeof(TImplementation), descriptor.ImplementationType, $"Incorrect implementation for {typeof(TService).Name}.");
    }

    public void HasScoped<TService, TImplementation>()
    {
        var descriptor = Services.FirstOrDefault(d => d.ServiceType == typeof(TService));
        Assert.IsNotNull(descriptor, $"{typeof(TService).Name} was not registered.");
        Assert.AreEqual(ServiceLifetime.Scoped, descriptor.Lifetime, $"Expected scoped for {typeof(TService).Name}.");
        Assert.AreEqual(typeof(TImplementation), descriptor.ImplementationType, $"Incorrect implementation for {typeof(TService).Name}.");
    }
}
