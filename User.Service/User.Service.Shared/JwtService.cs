using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using User.Service.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using User.Service.Domain.Interfaces;

namespace User.Service.Shared;

public class JwtService : IJwtService
{
    private readonly string _secretKey;
    private readonly int _accessTokenExpirationMinutes;
    private readonly int _refreshTokenExpirationDays;

    public JwtService(IConfiguration configuration)
    {
        _secretKey = configuration["JwtOptions:SecretKey"];
        _accessTokenExpirationMinutes = int.Parse(configuration["JwtOptions:AccessTokenExpirationMinutes"]);
        _refreshTokenExpirationDays = int.Parse(configuration["JwtOptions:RefreshTokenExpirationDays"]);
    }

    public (string AccessToken, string RefreshToken) GenerateTokens(AppUser user)
    {
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };
        
        foreach (var role in user.Roles.Select(ur => ur.Name))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        var accessTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_accessTokenExpirationMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var accessTokenHandler = new JwtSecurityTokenHandler();
        var accessToken = accessTokenHandler.WriteToken(accessTokenHandler.CreateToken(accessTokenDescriptor));
        
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        return (accessToken, refreshToken);
    }
}