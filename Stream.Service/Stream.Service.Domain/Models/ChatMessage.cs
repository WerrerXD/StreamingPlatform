namespace Stream.Service.Domain.Models;

public class ChatMessage
{
    public string UserId { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime Timestamp { get; set; }
}