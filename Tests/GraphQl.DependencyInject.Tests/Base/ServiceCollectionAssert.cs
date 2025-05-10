using Microsoft.Extensions.DependencyInjection;

namespace GraphQl.DependencyInject.Tests.Base;

public abstract class ServiceCollectionAssert
{
    public static void HasSingleton<TService, TImplementation>(IServiceCollection services)
    {
        var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
        Assert.IsNotNull(descriptor, $"{typeof(TService).Name} was not registered.");
        Assert.AreEqual(ServiceLifetime.Singleton, descriptor.Lifetime, $"Expected singleton for {typeof(TService).Name}.");
        Assert.AreEqual(typeof(TImplementation), descriptor.ImplementationType, $"Incorrect implementation for {typeof(TService).Name}.");
    }

    public static void HasScoped<TService, TImplementation>(IServiceCollection services)
    {
        var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
        Assert.IsNotNull(descriptor, $"{typeof(TService).Name} was not registered.");
        Assert.AreEqual(ServiceLifetime.Scoped, descriptor.Lifetime, $"Expected scoped for {typeof(TService).Name}.");
        Assert.AreEqual(typeof(TImplementation), descriptor.ImplementationType, $"Incorrect implementation for {typeof(TService).Name}.");
    }

    public static void HasTransient<TService, TImplementation>(IServiceCollection services)
    {
        var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
        Assert.IsNotNull(descriptor, $"{typeof(TService).Name} was not registered.");
        Assert.AreEqual(ServiceLifetime.Transient, descriptor.Lifetime, $"Expected transient for {typeof(TService).Name}.");
        Assert.AreEqual(typeof(TImplementation), descriptor.ImplementationType, $"Incorrect implementation for {typeof(TService).Name}.");
    }
}
