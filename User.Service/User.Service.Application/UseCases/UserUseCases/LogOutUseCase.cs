using User.Service.Application.Abstractions;
using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.UserUseCases;

public class LogOutUseCase : ILogOutUseCase
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IBlacklistedTokenRepository _blacklistedTokenRepository;
    private readonly IJwtService _jwtService;

    public LogOutUseCase(IRefreshTokenRepository refreshTokenRepository, IBlacklistedTokenRepository blacklistedTokenRepository, IJwtService jwtService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _blacklistedTokenRepository = blacklistedTokenRepository;
        _jwtService = jwtService;
    }

    public async Task ExecuteAsync(string accessToken, Guid userId, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetNotExpiredTokenAsync(userId, cancellationToken)
            ?? throw new NotFoundException("Your refresh token was not found");
        
        await _refreshTokenRepository.DeleteAsync(refreshToken, cancellationToken);
        
        if (!string.IsNullOrEmpty(accessToken))
        {
            var expiration = _jwtService.GetTokenExpiration(accessToken);

            await _blacklistedTokenRepository.AddAsync(accessToken, expiration);
        }
    }
}