using System.Reflection;
using GraphQl.Abstractions;
using Identity.Abstractions;
using Identity.Core;
using Identity.Mappers;

namespace Identity.Extensions;

public static class IServiceCollectionExtentions
{
    public static IServiceCollection GetServiceCollection(this IServiceCollection services)
    {
        services.AddScoped<IIdentityManager, DefaultIdentityManager>();
        return services;
    }
    public static IServiceCollection AddIdentityMapper(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(IdentityProfile));
        return service;
    }
    public static void RegisterIdentityRegisterModules(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        try
        {
            var assemblies = Directory
                .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Select(Assembly.LoadFrom)
                .ToArray();

            var moduleTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(
                    t =>
                        typeof(IServiceRegistrationModule).IsAssignableFrom(t)
                        && !t.IsInterface
                        && !t.IsAbstract
                );

            foreach (var moduleType in moduleTypes)
            {
                var moduleInstance = Activator.CreateInstance(moduleType);
                if (moduleInstance is not null)
                {
                    var module = (IServiceRegistrationModule)moduleInstance;
                    module?.RegisterServices(services, configuration);
                }
            }
        }
        catch (ReflectionTypeLoadException ex)
        {
            Console.WriteLine("ReflectionTypeLoadException caught:");
            foreach (var loaderException in ex.LoaderExceptions)
            {
                Console.WriteLine("Error:" + loaderException?.ToString() ?? "No Exception");
            }
        }
    }

}
