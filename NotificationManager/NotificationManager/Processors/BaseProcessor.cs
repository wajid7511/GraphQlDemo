using GraphQlDemo.Shared.Messaging;

namespace NotificationManager.Processors;


public abstract class BaseProcessor
{
    public abstract bool CanProcess(MessageType messageType);

    public virtual ValueTask<bool> ProcessAsync(MessageDto messageDto)
    {
        return ValueTask.FromResult(false);
    }
}
