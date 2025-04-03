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
        await _userRepository.UnblockExpiredUsers(cancellationToken);
    }
}