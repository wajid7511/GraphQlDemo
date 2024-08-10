using GraphQl.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQl.Core;

public class CoreRegisterServices : IServiceRegistrationModule
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IProductManager, DefaultProductManager>();
        services.AddScoped<IGroceryManager, DefaultGroceryManager>();
        services.AddSingleton<IDateTimeProvider, DefaultDateTimeProvider>();
    }
}
