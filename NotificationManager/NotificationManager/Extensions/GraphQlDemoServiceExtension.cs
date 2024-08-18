using System.Reflection;
using GraphQl.Abstractions;
using GraphQlDemo.Shared.Options;
using NotificationManager.Processors;

namespace NotificationManager.Extensions;

public static class GraphQlDemoServiceExtension
{
    public static IServiceCollection RegisterRabbitMqServices(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.CONFIG_PATH));
        return services;
    }

    public static IServiceCollection RegisterNotificationProcessors(
        this IServiceCollection services
    )
    {
        var baseProcessorType = typeof(BaseProcessor);
        var processors = baseProcessorType.Assembly
            .GetTypes()
            .Where(t => baseProcessorType.IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var processor in processors)
        {
            services.AddScoped(baseProcessorType, processor);
        }

        return services;
    }

    public static void RegisterGraphQlDemoIServicesRegisterModules(
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
