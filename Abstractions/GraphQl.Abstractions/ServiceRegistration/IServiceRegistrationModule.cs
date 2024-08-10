using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQl.Abstractions;

public interface IServiceRegistrationModule
{
    void RegisterServices(
        IServiceCollection serviceRegistration,
        ConfigurationManager configuration
    );
}
