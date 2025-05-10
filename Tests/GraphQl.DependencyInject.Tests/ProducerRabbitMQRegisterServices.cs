using GraphQl.Abstractions;
using GraphQl.DependencyInject.Tests.Base;
using Producer.RabbitMq;

namespace GraphQl.DependencyInject.Tests;

[TestClass]
public class ProducerRabbitMQRegisterServicesTests : ServiceCollectionAssert
{
    [TestMethod]
    public void RegisterServices_Should_Register_Services_Into_DI()
    {
        // Arrange 
        var module = new ProducerRabbitMQRegisterServices();

        // Act
        module.RegisterServices(Services, Configuration);

        // Assert
        HasSingleton<IMessageProducer, DefaultProducerRabbitMQ>();
    }
}