using Stream.Service.Domain.Models;

namespace Stream.Service.Domain.Interfaces;

public interface IChatRepository
{
    Task<List<ChatMessage>> GetMessagesByStreamIdAsync(string streamId, CancellationToken cancellationToken);
    Task AddMessageAsync(ChatMessage message, CancellationToken cancellationToken);
}