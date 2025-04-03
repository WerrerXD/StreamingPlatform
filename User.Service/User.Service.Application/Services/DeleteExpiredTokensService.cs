using User.Service.Domain.Interfaces;

namespace User.Service.Application.Services;

public class DeleteExpiredTokensService : IDeleteExpiredTokensService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public DeleteExpiredTokensService(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task DeleteExpiredTokens(CancellationToken cancellationToken)
    {
        await _refreshTokenRepository.DeleteExpiredTokens(cancellationToken);
    }
}