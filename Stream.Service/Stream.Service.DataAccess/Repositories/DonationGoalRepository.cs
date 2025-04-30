using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stream.Service.DataAccess.Settings;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.DataAccess.Repositories;

public class DonationGoalRepository : Repository<DonationGoal>, IDonationGoalRepository
{
    public DonationGoalRepository(IMongoClient mongoClient, IOptions<DatabaseSettings> settings) 
        : base(mongoClient, settings, "donation_goals")
    {
    }

    public async Task<List<DonationGoal>> GetAllDonationGoalsByStreamerIdAsync(string streamerId, CancellationToken cancellationToken)
    {
        return await _collection.Find(d => d.StreamerId == streamerId)
            .ToListAsync(cancellationToken);
    }

    public async Task<DonationGoal> GetActiveDonationGoalByStreamerIdAsync(string streamerId, CancellationToken cancellationToken)
    {
        return await _collection
            .Find(d => d.StreamerId == streamerId && d.IsActive)
            .FirstOrDefaultAsync(cancellationToken);
    }
}