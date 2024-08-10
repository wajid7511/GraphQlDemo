using System.Reflection;
using GraphQl.Abstractions;
using GraphQl.Database;
using Microsoft.EntityFrameworkCore;

namespace GraphQlDemo;

public static class GraphQlDemoServiceExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(GraphQlDemoProfile));
        return service;
    }

    public static void RegisterIServicesRegisterModules(this IServiceCollection services)
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
                module?.RegisterServices(services);
            }
        }
    }

    public static IServiceCollection AddGraphQl(this IServiceCollection service)
    {
        service
            .AddGraphQLServer()
            .AddMutationType<Mutation>()
            .AddTypeExtension<ProductMutation>()
            .AddTypeExtension<GroceryMutation>()
            .AddQueryType<Query>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();
        // .AddType<ProductType>()
        // .AddType<GroceryType>()
        // .AddAuthorization() ;
        return service;
    }

    public static IServiceCollection AddDatabase(
        this IServiceCollection service,
        ConfigurationManager configuration
    )
    {
        service.AddDbContext<GraphQlDatabaseContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("Default"))
        );
        return service;
    }
}
