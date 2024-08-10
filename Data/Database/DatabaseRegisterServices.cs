using GraphQl.Abstractions;
using GraphQl.Database.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQl.Core;

public class DatabaseRegisterServices : IServiceRegistrationModule
{
    public void RegisterServices(IServiceCollection services, ConfigurationManager configuration)
    {
        // Register Databse DAL
        services.AddScoped<ProductDAL>();
        services.AddScoped<GroceryDAL>();
    }
}
