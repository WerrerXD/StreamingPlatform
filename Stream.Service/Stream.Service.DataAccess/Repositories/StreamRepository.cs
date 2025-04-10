using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;
using Stream.Service.Domain.Settings;
using System.Threading;
using System.Threading.Tasks;

namespace Stream.Service.DataAccess.Repositories;

public class StreamRepository : IStreamRepository
{
    private readonly IMongoCollection<StreamModel> _collection;

    public StreamRepository(IMongoClient mongoClient, IOptions<DatabaseSettings> settings)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<StreamModel>("streams");
    }

    public async Task<string> CreateAsync(StreamModel stream, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(stream, cancellationToken: cancellationToken);
        return stream.Id;
    }

    public async Task<StreamModel?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _collection
            .Find(s => s.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<StreamModel>> GetAllByStreamerAsync(string streamerId, CancellationToken cancellationToken)
    {
        return await _collection
            .Find(s => s.StreamerId == streamerId)
            .ToListAsync(cancellationToken);
    }

    public async Task EndStreamAsync(string streamId, CancellationToken cancellationToken)
    {
        var update = Builders<StreamModel>.Update.Set(s => s.EndTime, DateTime.UtcNow);
        
        await _collection.UpdateOneAsync(
            s => s.Id == streamId,
            update,
            cancellationToken: cancellationToken
        );
    }
    
    public async Task<List<StreamModel>> GetAllActiveAsync(CancellationToken cancellationToken)
    {
        return await _collection.Find(s => s.EndTime == null).ToListAsync(cancellationToken);
    }
}