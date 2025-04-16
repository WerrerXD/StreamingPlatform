using User.Service.Application.Abstractions;
using User.Service.Domain.Interfaces;

namespace User.Service.Application.Services;

public class UnblockExpiredUsersJob : IUnblockExpiredUsersJob
{
    private readonly IUserRepository _userRepository;

    public UnblockExpiredUsersJob(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task UnblockExpiredUsersAsync(CancellationToken cancellationToken)
    {
        var expiredUsers = await _userRepository.GetAllExpiredUsers(cancellationToken);
        
        expiredUsers.ForEach(u => u.IsBlocked = false);
        
        await _userRepository.UpdateRangeAsync(expiredUsers, cancellationToken);
    }
}