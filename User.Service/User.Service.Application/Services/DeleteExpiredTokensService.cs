using User.Service.Application.Abstractions;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.Services;

public class DeleteExpiredTokensService : IDeleteExpiredTokensService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public DeleteExpiredTokensService(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task DeleteExpiredTokensAsync(CancellationToken cancellationToken)
    {
        var tokens = await _refreshTokenRepository
            .GetAllAsync(cancellationToken);
        
        var expiredTokens = tokens
            .Where(r => r.ExpiresAt < DateTime.UtcNow)
            .ToList();
        
        await _refreshTokenRepository.DeleteRangeAsync(expiredTokens, cancellationToken);
    }
}