namespace Stream.Service.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
}