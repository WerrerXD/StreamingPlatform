using Microsoft.EntityFrameworkCore;
using User.Service.Domain.Entities;
using User.Service.Domain.Interfaces;

namespace User.Service.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken)
    {
        _context.Set<T>().RemoveRange(entities);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(List<T> entities, CancellationToken cancellationToken)
    {
        _context.Set<T>().UpdateRange(entities);
        await _context.SaveChangesAsync(cancellationToken);
    }
}