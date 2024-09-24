namespace GraphQlDemo.Shared.Messaging;

public class MessageDto
{
    public MessageType MessageType { get; set; }
    public string ReferenceId { get; set; } = string.Empty;
}
