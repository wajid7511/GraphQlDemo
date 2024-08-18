using System.Text;
using System.Text.Json;
using GraphQlDemo.Shared.Messaging;
using GraphQlDemo.Shared.Options;
using Microsoft.Extensions.Options;
using NotificationManager.Processors;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationManager;

public class Worker : BackgroundService
{
    private readonly IConfiguration _configuration;
    private IConnection _connection = null!;
    private IModel _channel = null!;
    private readonly ILogger<Worker>? _logger;
    private readonly IEnumerable<BaseProcessor> _processors;

    private readonly IServiceProvider _serviceProvider;
    private readonly RabbitMqOptions _rabbitMqOptions;

    public Worker(
        IConfiguration configuration,
        IOptions<RabbitMqOptions> options,
        IServiceProvider serviceProvider,
        ILogger<Worker>? logger = null
    )
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _rabbitMqOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _serviceProvider =
            serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        _logger = logger;
        _processors = GetProcessors();
        InitializeRabbitMQ();
    }

    private IEnumerable<BaseProcessor> GetProcessors()
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IEnumerable<BaseProcessor>>();
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Unable to get the processors");
            return [];
        }
    }

    private void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqOptions.HostName,
            UserName = _rabbitMqOptions.UserName,
            Password = _rabbitMqOptions.Password,
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: _rabbitMqOptions.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            _logger?.LogInformation($"Received message: {message}");

            // Deserialize the message if it's a complex object
            var messageDto = JsonSerializer.Deserialize<MessageDto>(message);
            if (messageDto != null)
            {
                foreach (var processor in _processors)
                {
                    var canProcess = processor.CanProcess(messageDto.MessageType);
                    if (canProcess)
                    {
                        await processor.ProcessAsync(messageDto);
                        break;
                    }
                }
            }
            else
            {
                _logger?.LogInformation("Message Dto is null");
            }
        };

        _channel.BasicConsume(queue: _rabbitMqOptions.QueueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}
