namespace User.Service.Domain.Interfaces;

public interface IBlacklistedTokenRepository
{
    Task AddAsync(string token, TimeSpan expiration);
    Task<bool> IsBlacklistedAsync(string token);
}