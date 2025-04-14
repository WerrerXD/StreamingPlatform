using Microsoft.AspNetCore.SignalR;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;
using Stream.Service.Presentation.Hubs;

namespace Stream.Service.Presentation.Services;

public class ChatNotificationService : IChatNotificationService
{
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatNotificationService(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessageToGroupAsync(string streamId, ChatMessage message)
    {
        await _hubContext.Clients.Group(streamId).SendAsync("ReceiveMessage", message);
    }
}