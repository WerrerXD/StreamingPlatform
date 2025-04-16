using User.Service.Application.Abstractions;
using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.UserUseCases
{
    public class RefreshTokenUseCase : IRefreshTokenUseCase
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenUseCase(IUserRepository userRepository, IJwtService jwtService, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<(string AccessToken, string RefreshToken)> ExecuteAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var token = await _refreshTokenRepository.GetTokenAsync(refreshToken, cancellationToken)
                ?? throw new UnauthorizedException("Your refresh token has expired, pls log in again");
            
            var user = await _userRepository.GetByIdAsync(token.UserId, cancellationToken);

            var (newAccessToken, newRefreshToken) = _jwtService.GenerateTokens(user);
            
            token.Token = newRefreshToken;
            
            await _refreshTokenRepository.UpdateAsync(token, cancellationToken);

            return (newAccessToken, newRefreshToken);
        }
    }
}
