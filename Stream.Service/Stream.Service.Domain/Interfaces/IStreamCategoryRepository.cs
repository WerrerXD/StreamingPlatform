using Stream.Service.Domain.Models;

namespace Stream.Service.Domain.Interfaces;

public interface IStreamCategoryRepository
{
    Task<string> CreateAsync(StreamCategory streamCategory, CancellationToken cancellationToken);
    Task<List<StreamCategory>> GetAllAsync(CancellationToken cancellationToken);
    Task<StreamCategory?> GetStreamCategoryById(string streamCategoryId, CancellationToken cancellationToken);
    Task<StreamCategory?> GetStreamCategoryByName(string streamCategoryName, CancellationToken cancellationToken);
    Task SetStreamCategoryName(string streamCategoryId, string streamCategoryName, CancellationToken cancellationToken);
    Task SetStreamCategoryDescription(string streamCategoryId, string streamCategoryDescription, CancellationToken cancellationToken);
    Task DeleteStreamCategory(StreamCategory streamCategory, CancellationToken cancellationToken);
}