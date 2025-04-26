using Stream.Service.Domain.Models;

namespace Stream.Service.Domain.Interfaces;

public interface IStreamCategoryRepository: IRepository<StreamCategory>
{
    Task<StreamCategory?> GetStreamCategoryByIdAsync(string streamCategoryId, CancellationToken cancellationToken);
    Task<StreamCategory?> GetStreamCategoryByNameAsync(string streamCategoryName, CancellationToken cancellationToken);
}