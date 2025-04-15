using User.Service.Domain.Entities;

namespace User.Service.Domain.Interfaces;

public interface IRepository<T> where T : class  {
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    Task DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken);
    Task UpdateRangeAsync(List<T> entities, CancellationToken cancellationToken);
}