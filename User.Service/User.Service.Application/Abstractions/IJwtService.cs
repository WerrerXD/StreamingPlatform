using User.Service.Domain.Entities;

namespace User.Service.Application.Abstractions;

public interface IJwtService
{
    (string AccessToken, string RefreshToken) GenerateTokens(AppUser user);
    TimeSpan GetTokenExpiration(string accessToken);
}