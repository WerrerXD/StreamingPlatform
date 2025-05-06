using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Abstractions;

public interface IChatNotificationService
{
    Task SendMessageToGroupAsync(string streamId, ChatMessage message);
}