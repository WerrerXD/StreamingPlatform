using StackExchange.Redis;
using User.Service.Domain.Interfaces;

namespace User.Service.Infrastructure.Repositories;

public class BlacklistedTokenRepository : IBlacklistedTokenRepository
{
    private readonly IDatabase _redis;

    public BlacklistedTokenRepository(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }

    public async Task AddAsync(string token, TimeSpan expiration)
    {
        await _redis.StringSetAsync(token, "blacklisted", expiration);
    }

    public async Task<bool> IsBlacklistedAsync(string token)
    {
        return await _redis.KeyExistsAsync(token);
    }
}