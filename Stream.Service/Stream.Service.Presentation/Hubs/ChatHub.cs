using Microsoft.AspNetCore.SignalR;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.Presentation.Hubs;

public class ChatHub: Hub
{
    private readonly IChatNotificationService _notificationService;

    public ChatHub(IChatNotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public override async Task OnConnectedAsync()
    {
        var streamId = Context.GetHttpContext().Request.Query["streamId"];
        await Groups.AddToGroupAsync(Context.ConnectionId, streamId);
        await base.OnConnectedAsync();
    }

    public async Task SendMessage(string streamId, string userId, string message)
    {
        var chatMessage = new ChatMessage
        {
            StreamId = streamId,
            UserId = userId,
            Message = message,
            Timestamp = DateTime.UtcNow
        };
        
        await _notificationService.SendMessageToGroupAsync(streamId, chatMessage);
    }
}