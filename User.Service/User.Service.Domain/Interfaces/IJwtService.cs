using User.Service.Domain.Entities;

namespace User.Service.Domain.Interfaces;

public interface IJwtService
{
    (string AccessToken, string RefreshToken) GenerateTokens(AppUser user);
}