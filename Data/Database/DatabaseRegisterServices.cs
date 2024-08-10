using GraphQl.Abstractions;
using GraphQl.Database.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQl.Core;

public class DatabaseRegisterServices : IServiceRegistrationModule
{
    public void RegisterServices(IServiceCollection services)
    {
        // Register Databse DAL
        services.AddScoped<ProductDAL>();
        services.AddScoped<GroceryDAL>();
    }
}
