using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;
using Stream.Service.Domain.Settings;

namespace Stream.Service.DataAccess.Repositories;

public class DonationRepository : Repository<Donation>, IDonationRepository
{
    public DonationRepository(IMongoClient mongoClient, IOptions<DatabaseSettings> settings) 
        : base(mongoClient, settings, "donations")
    {
    }

    public async Task<List<Donation>> GetDonationsByStreamIdAsync(string streamId, CancellationToken cancellationToken)
    {
        return await _collection.Find(m => m.StreamId == streamId)
            .SortByDescending(m => m.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}