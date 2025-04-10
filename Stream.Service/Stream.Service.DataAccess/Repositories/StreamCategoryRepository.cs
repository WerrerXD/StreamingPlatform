using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stream.Service.Domain.Models;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Settings;

namespace Stream.Service.DataAccess.Repositories;

public class StreamCategoryRepository : IStreamCategoryRepository
{
    private readonly IMongoCollection<StreamCategory> _collection;

    public StreamCategoryRepository(IMongoClient mongoClient, IOptions<DatabaseSettings> settings)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<StreamCategory>("stream_categories");
    }
    
    public async Task<string> CreateAsync(StreamCategory streamCategory, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(streamCategory, cancellationToken: cancellationToken);
        return streamCategory.Id;
    }

    public async Task<List<StreamCategory>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _collection.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<StreamCategory?> GetStreamCategoryById(string streamCategoryId, CancellationToken cancellationToken)
    {
        return await _collection
            .Find(sc => sc.Id == streamCategoryId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<StreamCategory?> GetStreamCategoryByName(string streamCategoryName, CancellationToken cancellationToken)
    {
        return await _collection
            .Find(sc => sc.Name == streamCategoryName)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task SetStreamCategoryName(string streamCategoryId, string streamCategoryName, CancellationToken cancellationToken)
    {
        var update = Builders<StreamCategory>
            .Update.Set(sc => sc.Name, streamCategoryName);
        
        await _collection.UpdateOneAsync(sc => sc.Id == streamCategoryId, update, cancellationToken: cancellationToken);
    }

    public async Task SetStreamCategoryDescription(string streamCategoryId, string streamCategoryDescription, CancellationToken cancellationToken)
    {
        var update = Builders<StreamCategory>
            .Update.Set(sc => sc.Description, streamCategoryDescription);
        
        await _collection.UpdateOneAsync(sc => sc.Id == streamCategoryId, update, cancellationToken: cancellationToken);
    }

    public async Task DeleteStreamCategory(StreamCategory streamCategory, CancellationToken cancellationToken)
    {
        await _collection.DeleteOneAsync(sc => sc.Id == streamCategory.Id, cancellationToken);
    }
}