namespace GraphQl.Abstractions;

public interface IMessageProducer
{
    bool SendMessage<MessageDto>(MessageDto message);
}
