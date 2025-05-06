using Stream.Service.Domain.Models;

namespace Stream.Service.Domain.Interfaces;

public interface IDonationRepository: IRepository<Donation>
{
    Task<List<Donation>> GetDonationsByStreamIdAsync(string streamId, CancellationToken cancellationToken);
}