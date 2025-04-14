using Stream.Service.Domain.Models;

namespace Stream.Service.Domain.Interfaces;

public interface IChatNotificationService
{
    Task SendMessageToGroupAsync(string streamId, ChatMessage message);
}