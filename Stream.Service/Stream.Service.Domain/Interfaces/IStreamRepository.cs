using Stream.Service.Domain.Models;

namespace Stream.Service.Domain.Interfaces;

public interface IStreamRepository
{
    Task<string> CreateAsync(StreamModel stream, CancellationToken cancellationToken);
    Task<StreamModel?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<List<StreamModel>> GetAllByStreamerAsync(string streamerId, CancellationToken cancellationToken);
    Task EndStreamAsync(string streamId, CancellationToken cancellationToken);
    Task<List<StreamModel>> GetAllActiveAsync(CancellationToken cancellationToken);
}