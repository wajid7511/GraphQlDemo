using GraphQl.Mongo.Database.DALs;
using GraphQlDemo.Shared.Messaging;

namespace NotificationManager.Processors;


public class EmailProcessor(CustomerDAL customerDAL, ILogger<EmailProcessor>? logger = null) : BaseProcessor
{
    private readonly CustomerDAL _customerDAL = customerDAL ?? throw new ArgumentNullException(nameof(customerDAL));
    private readonly ILogger<EmailProcessor>? _logger = logger;
    public override bool CanProcess(MessageType messageType)
    {
        return messageType == MessageType.Order;
    }

    public override async ValueTask<bool> ProcessAsync(MessageDto messageDto)
    {
        var order = await _customerDAL.GetCustomerOrderByIdAsync(Guid.Parse(messageDto.ReferenceId));
        if (order == null)
        {
            _logger?.LogDebug("No order found with id {0}", messageDto.ReferenceId);
            return false;
        }
        _logger?.LogDebug("I will send for order number {0}, later", order.Id.ToString());
        return await base.ProcessAsync(messageDto);
    }
}
