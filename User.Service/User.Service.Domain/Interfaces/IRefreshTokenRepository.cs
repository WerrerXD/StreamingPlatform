using User.Service.Domain.Entities;

namespace User.Service.Domain.Interfaces;

public interface IRefreshTokenRepository: IRepository<RefreshToken>
{
    Task<RefreshToken> GetNotExpiredToken(Guid userId, CancellationToken cancellationToken);
    Task<bool> IsExistByUserId(Guid id, CancellationToken cancellationToken);
    Task<RefreshToken> GetTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task DeleteExpiredTokens(CancellationToken cancellationToken);
}