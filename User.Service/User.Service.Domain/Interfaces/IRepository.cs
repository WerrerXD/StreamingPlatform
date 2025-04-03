using User.Service.Domain.Entities;

namespace User.Service.Domain.Interfaces;

public interface IRepository<T> where T : class  {
    Task<List<T>> GetAll(CancellationToken cancellationToken);
    Task Create(T entity, CancellationToken cancellationToken);
    Task Update(T entity);
    Task Delete(T entity);
    Task Save(CancellationToken cancellationToken);
}