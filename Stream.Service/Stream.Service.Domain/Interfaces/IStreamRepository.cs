using Stream.Service.Domain.Models;

namespace Stream.Service.Domain.Interfaces;

public interface IStreamRepository: IRepository<StreamModel>
{
    Task<StreamModel?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<List<StreamModel>> GetAllByStreamerAsync(string streamerId, CancellationToken cancellationToken);
    Task<List<StreamModel>> GetAllActiveAsync(CancellationToken cancellationToken);
}