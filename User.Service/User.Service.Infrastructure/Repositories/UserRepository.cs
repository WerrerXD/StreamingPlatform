using Microsoft.EntityFrameworkCore;
using User.Service.Domain.Entities;
using User.Service.Domain.Interfaces;

namespace User.Service.Infrastructure.Repositories;

public class UserRepository: Repository<AppUser>,IUserRepository
{
    
    public UserRepository(ApplicationDbContext context)
        :base(context)
    {
    }
    

    public async Task<AppUser> GetByIdAsync(Guid id, CancellationToken cancellationToken)
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

    public async Task AddAdminToUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin", cancellationToken);
        var user = await _context.Users
                       .Include(u => u.Roles)
                       .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        user.Roles.Add(role);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task FollowUser(Follow follow, CancellationToken cancellationToken)
    {
        await _context.Follows.AddAsync(follow, cancellationToken);
        var follower = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == follow.FollowerId, cancellationToken);
        var followee = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == follow.FollowingId, cancellationToken);
        follower.FollowingCount++;
        followee.FollowersCount++;
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task UnfollowUser(Follow follow, CancellationToken cancellationToken)
    {
        _context.Follows.Remove(follow);
        var unfollower = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == follow.FollowerId, cancellationToken);
        var unfollowee = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == follow.FollowingId, cancellationToken);
        unfollower.FollowingCount--;
        unfollowee.FollowersCount--;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsFollowingUser(Guid followerId, Guid followeeId, CancellationToken cancellationToken)
    {
        return await _context.Follows
            .AsNoTracking()
            .AnyAsync(f => f.FollowerId == followerId && f.FollowingId == followeeId, cancellationToken);
    }

    public async Task BanUserUntil(Guid userId, int daysBanned, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        user.IsBlocked = true;
        user.BlockedUntil = DateTime.UtcNow.AddDays(daysBanned);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SetAvatarUrl(Guid userId, string avatarUrl, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        user.AvatarUrl = avatarUrl;
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task SetUserName(Guid userId, string userName, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        user.UserName = userName;
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task SetDescription(Guid userId, string description, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        user.Description = description;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UnblockExpiredUsers(CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .Where(u => u.BlockedUntil < DateTime.UtcNow && u.IsBlocked)
            .ToListAsync(cancellationToken);
        foreach (var user in users)
        {
            user.IsBlocked = false;
        }
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}