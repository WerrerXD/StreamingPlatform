using User.Service.Domain.Entities;

namespace User.Service.Domain.Interfaces;

public interface IRefreshTokenRepository: IRepository<RefreshToken>
{
    Task<RefreshToken> GetNotExpiredTokenAsync(Guid userId, CancellationToken cancellationToken);
    Task<bool> IsExistByUserIdAsync(Guid id, CancellationToken cancellationToken);
    Task<RefreshToken> GetTokenAsync(string refreshToken, CancellationToken cancellationToken);
}