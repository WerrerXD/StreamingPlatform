using User.Service.Application.Exceptions;
using User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;
using User.Service.Domain.Entities;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.UseCases.UserUseCases;

public class LoginUserUseCase : ILoginUserUseCase
{

    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;


    public LoginUserUseCase(IJwtService jwtService, IPasswordHasher passwordHasher, IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<(string AccessToken, string RefreshToken)> ExecuteAsync(string email, string password, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            throw new BadRequestException("User data can not be empty");
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken) ?? throw new UnauthorizedException("Invalid data");
        
        var result = _passwordHasher.Verify(password, user.PasswordHash);

        if (result == false)
        {
            throw new UnauthorizedException("Invalid data");
        }

        if (user.IsBlocked && user.BlockedUntil >= DateTime.UtcNow)
        {
            throw new BadRequestException($"You are blocked in the system until {user.BlockedUntil}");
        }

        var (jwtToken, refreshToken) = _jwtService.GenerateTokens(user);

        RefreshToken refreshTokenModel = new()
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(1)
        };

        await _refreshTokenRepository.Create(refreshTokenModel, cancellationToken);
        await _refreshTokenRepository.Save(cancellationToken);

        return (jwtToken,refreshToken);
    }
}