using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stream.Service.Domain.Models;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Settings;

namespace Stream.Service.DataAccess.Repositories;

public class StreamCategoryRepository : Repository<StreamCategory>, IStreamCategoryRepository
{
    public StreamCategoryRepository(IMongoClient mongoClient, IOptions<DatabaseSettings> settings) 
        : base(mongoClient, settings, "stream_categories")
    {
    }
    
    public async Task<StreamCategory?> GetStreamCategoryByIdAsync(string streamCategoryId, CancellationToken cancellationToken)
    {
        return await _collection
            .Find(sc => sc.Id == streamCategoryId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<StreamCategory?> GetStreamCategoryByNameAsync(string streamCategoryName, CancellationToken cancellationToken)
    {
        return await _collection
            .Find(sc => sc.Name == streamCategoryName)
            .FirstOrDefaultAsync(cancellationToken);
    }
}