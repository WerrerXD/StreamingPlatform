using Stream.Service.Domain.Models;

namespace Stream.Service.Domain.Interfaces;

public interface IChatRepository: IRepository<ChatMessage>
{
    Task<List<ChatMessage>> GetMessagesByStreamIdAsync(string streamId, CancellationToken cancellationToken);
}