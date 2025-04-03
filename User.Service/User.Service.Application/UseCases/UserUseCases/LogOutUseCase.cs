using System.IdentityModel.Tokens.Jwt;
using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.UserUseCases;

public class LogOutUseCase : ILogOutUseCase
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IBlacklistedTokenRepository _blacklistedTokenRepository;

    public LogOutUseCase(IRefreshTokenRepository refreshTokenRepository, IBlacklistedTokenRepository blacklistedTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _blacklistedTokenRepository = blacklistedTokenRepository;
    }

    public async Task ExecuteAsync(string accessToken, Guid userId, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetNotExpiredToken(userId, cancellationToken)
            ?? throw new NotFoundException("Your refresh token was not found");
        await _refreshTokenRepository.Delete(refreshToken);
        await _refreshTokenRepository.Save(cancellationToken);
        
        if (!string.IsNullOrEmpty(accessToken))
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);
            var expiration = jwtToken.ValidTo - DateTime.UtcNow;

            await _blacklistedTokenRepository.AddAsync(accessToken, expiration);
        }
    }
}