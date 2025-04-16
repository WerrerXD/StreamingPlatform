using Microsoft.EntityFrameworkCore;
using User.Service.Domain.Entities;
using User.Service.Domain.Interfaces;

namespace User.Service.Infrastructure.Repositories;

public class UserRepository: Repository<AppUser>, IUserRepository
{
    public UserRepository(ApplicationDbContext context)
        :base(context)
    {
    }
    
    public async Task<AppUser> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
    
    public async Task<AppUser> GetByIdWithRolesAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
    
    public async Task<bool> IsExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == id, cancellationToken);
    }
    
    public async Task<AppUser> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        return user;
    }
    
    public async Task<AppUser> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.UserName == username, cancellationToken);

        return user;
    }

    public async Task<List<AppUser>> GetAllExpiredUsers(CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(u => u.BlockedUntil < DateTime.UtcNow && u.IsBlocked)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<bool> IsFollowingUserAsync(Guid followerId, Guid followeeId, CancellationToken cancellationToken)
    {
        return await _context.Follows
            .AsNoTracking()
            .AnyAsync(f => f.FollowerId == followerId && f.FollowingId == followeeId, cancellationToken);
    }

    public async Task<Role> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
        
        return role;
    }

    public async Task FollowUserAsync(Follow follow, CancellationToken cancellationToken)
    {
        await _context.Follows.AddAsync(follow, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UnFollowUserAsync(Follow follow, CancellationToken cancellationToken)
    {
        _context.Follows.Remove(follow);
        await _context.SaveChangesAsync(cancellationToken);
    }
}