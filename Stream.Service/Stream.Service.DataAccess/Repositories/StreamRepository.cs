using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;
using Stream.Service.Domain.Settings;

namespace Stream.Service.DataAccess.Repositories;

public class StreamRepository : IStreamRepository
{
    private readonly IMongoCollection<StreamModel> _collection;

    public StreamRepository(IMongoClient mongoClient, IOptions<DatabaseSettings> settings)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<StreamModel>("streams");
    }

    public async Task<string> CreateAsync(StreamModel stream)
    {
        await _collection.InsertOneAsync(stream);
        return stream.Id;
    }

    public async Task<StreamModel?> GetByIdAsync(string id)
    {
        return await _collection.Find(s => s.Id == id).FirstOrDefaultAsync();
    }
    
}