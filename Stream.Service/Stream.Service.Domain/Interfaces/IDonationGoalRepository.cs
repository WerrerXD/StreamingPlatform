using Stream.Service.Domain.Models;

namespace Stream.Service.Domain.Interfaces;

public interface IDonationGoalRepository: IRepository<DonationGoal>
{
    Task<List<DonationGoal>> GetAllDonationGoalsByStreamerIdAsync(string streamerId, CancellationToken cancellationToken);
    Task<DonationGoal> GetActiveDonationGoalByStreamerIdAsync(string streamerId, CancellationToken cancellationToken);
}