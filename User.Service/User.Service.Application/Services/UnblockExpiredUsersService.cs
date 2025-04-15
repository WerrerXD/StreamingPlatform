using User.Service.Application.Abstractions;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.Services;

public class UnblockExpiredUsersService : IUnblockExpiredUsersService
{
    private readonly IUserRepository _userRepository;

    public UnblockExpiredUsersService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task UnblockExpiredUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        
        var expiredUsers = users
            .Where(u => u.BlockedUntil < DateTime.UtcNow && u.IsBlocked)
            .ToList();
        
        expiredUsers.ForEach(u => u.IsBlocked = false);
        
        await _userRepository.UpdateRangeAsync(expiredUsers, cancellationToken);
    }
}