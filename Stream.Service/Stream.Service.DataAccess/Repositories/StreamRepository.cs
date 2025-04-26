using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;
using Stream.Service.Domain.Settings;
using System.Threading;
using System.Threading.Tasks;

namespace Stream.Service.DataAccess.Repositories;

public class StreamRepository : Repository<StreamModel>, IStreamRepository
{
    public StreamRepository(IMongoClient mongoClient, IOptions<DatabaseSettings> settings) 
        : base(mongoClient, settings, "streams")
    {
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
    
    public async Task<List<StreamModel>> GetAllActiveAsync(CancellationToken cancellationToken)
    {
        return await _collection.Find(s => s.EndTime == null).ToListAsync(cancellationToken);
    }
}