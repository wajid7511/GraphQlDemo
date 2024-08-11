using GraphQl.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQl.Core;

public class CoreRegisterServices : IServiceRegistrationModule
{
    public void RegisterServices(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IProductManager, DefaultProductManager>();
        services.AddScoped<IGroceryManager, DefaultGroceryManager>();
        services.AddScoped<ICustomerManager, DefaultCustomerManager>();
        services.AddSingleton<IDateTimeProvider, DefaultDateTimeProvider>();
    }
}
