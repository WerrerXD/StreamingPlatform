using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using User.Service.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Elastic.Clients.Elasticsearch.Snapshot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using User.Service.Application.Abstractions;

namespace User.Service.Shared;

public class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;
    private const int RefreshTokenByteLength = 64;

    public JwtService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    
    public (string AccessToken, string RefreshToken) GenerateTokens(AppUser user)
    {
        var key = Encoding.ASCII.GetBytes(_jwtOptions.SecretKey);
        
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
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var accessTokenHandler = new JwtSecurityTokenHandler();
        var accessToken = accessTokenHandler.WriteToken(accessTokenHandler.CreateToken(accessTokenDescriptor));
        
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(RefreshTokenByteLength));

        return (accessToken, refreshToken);
    }
    
    public TimeSpan GetTokenExpiration(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(accessToken);

        return jwtToken.ValidTo - DateTime.UtcNow;
    }
}