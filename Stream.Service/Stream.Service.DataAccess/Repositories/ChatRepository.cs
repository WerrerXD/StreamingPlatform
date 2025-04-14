using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;
using Stream.Service.Domain.Settings;

namespace Stream.Service.DataAccess.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly IMongoCollection<ChatMessage> _collection;

    public ChatRepository(IMongoClient mongoClient, IOptions<DatabaseSettings> settings)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<ChatMessage>("chat_messages");
    }

    public async Task<List<ChatMessage>> GetMessagesByStreamIdAsync(string streamId, CancellationToken cancellationToken)
    {
        return await _collection.Find(m => m.StreamId == streamId)
            .SortByDescending(m => m.Timestamp)
            .ToListAsync(cancellationToken);
    }

    public async Task AddMessageAsync(ChatMessage message, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(message, cancellationToken: cancellationToken);
    }
}