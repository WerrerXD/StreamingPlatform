using Microsoft.EntityFrameworkCore;
using User.Service.Domain.Entities;
using User.Service.Domain.Interfaces;

namespace User.Service.Infrastructure.Repositories;

public class RefreshTokenRepository: Repository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<RefreshToken> GetNotExpiredToken(Guid userId, CancellationToken cancellationToken)
    {
        var refreshToken = await _context.RefreshTokens
            .OrderBy(rt => rt.ExpiresAt)
            .LastOrDefaultAsync(rt => rt.UserId == userId && rt.ExpiresAt > DateTime.UtcNow, cancellationToken);
        return refreshToken;
    }

    public async Task<bool> IsExistByUserId(Guid id, CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens.AnyAsync(rt => rt.UserId == id, cancellationToken);
    }

    public async Task<RefreshToken> GetTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken && rt.ExpiresAt > DateTime.UtcNow, cancellationToken);
    }

    public async Task DeleteExpiredTokens(CancellationToken cancellationToken)
    {
        var expiredTokens = await _context.RefreshTokens
            .Where(rt => rt.ExpiresAt < DateTime.UtcNow)
            .ToListAsync(cancellationToken);
        
        _context.RefreshTokens.RemoveRange(expiredTokens);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
}