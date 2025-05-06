using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Stream.Service.DataAccess.Settings;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly IMongoCollection<T> _collection;

    public Repository(IMongoClient mongoClient, IOptions<DatabaseSettings> settings, string collectionName)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _collection.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq("_id", ConvertToObjectId(GetIdValue(entity)));
        
        await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq("_id", ConvertToObjectId(GetIdValue(entity)));
        
        await _collection.DeleteOneAsync(filter, cancellationToken);
    }
    
    private object GetIdValue(T entity)
    {
        var idProperty = typeof(T).GetProperty("Id");

        return idProperty.GetValue(entity);
    }
    
    private object ConvertToObjectId(object id)
    {
        if (id is string idString && ObjectId.TryParse(idString, out var objectId))
        {
            return objectId;
        }

        return id;
    }
}