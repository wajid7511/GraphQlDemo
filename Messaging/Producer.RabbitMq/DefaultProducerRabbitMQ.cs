using System.Text;
using System.Text.Json;
using GraphQl.Abstractions;
using GraphQlDemo.Shared.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Producer.RabbitMq;

public class DefaultProducerRabbitMQ(IOptions<RabbitMqOptions> options, ILogger<DefaultProducerRabbitMQ>? logger) : IMessageProducer
{
    private readonly RabbitMqOptions _rabbitMqOptions = options.Value ?? throw new ArgumentNullException(nameof(options));
    private readonly ILogger<DefaultProducerRabbitMQ>? _logger = logger;
    public bool SendMessage<MessageDto>(MessageDto message)
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqOptions.HostName,
                UserName = _rabbitMqOptions.UserName,
                Password = _rabbitMqOptions.Password,
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(_rabbitMqOptions.ExchangeName, ExchangeType.Direct);
            channel.QueueDeclare(_rabbitMqOptions.QueueName, false, false, false, null);
            channel.QueueBind(_rabbitMqOptions.QueueName, _rabbitMqOptions.ExchangeName, string.Empty);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish(_rabbitMqOptions.ExchangeName, string.Empty, null, body);
            return true;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Unable to push message to queue");
            return false;
        }

    }
}
