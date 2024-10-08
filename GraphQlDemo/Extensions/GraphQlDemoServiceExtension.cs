﻿using System.Reflection;
using GraphQl.Abstractions;
using GraphQl.Database;
using GraphQlDemo.Shared.Options;
using Microsoft.EntityFrameworkCore;

namespace GraphQlDemo;

public static class GraphQlDemoServiceExtension
{
    public static IServiceCollection AddGraphQlDemoMapper(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(GraphQlDemoProfile));
        return service;
    }

    public static IServiceCollection AddGraphQlDemoOptions(
        this IServiceCollection service,
        IConfiguration configuration
    )
    {
        service.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.CONFIG_PATH));
        return service;
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

    public static IServiceCollection AddGraphQlDemoGraphQl(this IServiceCollection service)
    {
        service
            .AddGraphQLServer()
            .AddMutationType<Mutation>()
            .AddTypeExtension<ProductMutation>()
            .AddTypeExtension<GroceryMutation>()
            .AddTypeExtension<CustomerMutation>()
            .AddQueryType<Query>()
            .AddTypeExtension<ProductQuery>()
            .AddTypeExtension<GroceryQuery>()
            .AddTypeExtension<CustomerQuery>()
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
