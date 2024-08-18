using GraphQl.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Producer.RabbitMq;

public class ProducerRabbitMQRegisterServices : IServiceRegistrationModule
{
    public void RegisterServices(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<IMessageProducer, DefaultProducerRabbitMQ>();
    }
}
