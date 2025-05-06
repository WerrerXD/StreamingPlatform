using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stream.Service.DataAccess.Settings;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.DataAccess.Repositories;

public class ChatRepository : Repository<ChatMessage>, IChatRepository
{
    public ChatRepository(IMongoClient mongoClient, IOptions<DatabaseSettings> settings) 
        : base(mongoClient, settings, "chat_messages")
    {
    }

    public async Task<List<ChatMessage>> GetMessagesByStreamIdAsync(string streamId, CancellationToken cancellationToken)
    {
        return await _collection.Find(m => m.StreamId == streamId)
            .SortByDescending(m => m.Timestamp)
            .ToListAsync(cancellationToken);
    }
}