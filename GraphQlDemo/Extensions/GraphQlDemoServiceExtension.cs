using GraphQl.Abstractions;
using GraphQl.Core;
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

    public static IServiceCollection AddAbstractions(this IServiceCollection service)
    {
        service.AddScoped<IProductManager, DefaultProductManager>();
        service.AddScoped<IGroceryManager, DefaultGroceryManager>();
        return service;
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
