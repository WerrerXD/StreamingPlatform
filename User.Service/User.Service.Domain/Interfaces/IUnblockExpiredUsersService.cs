namespace User.Service.Domain.Interfaces;

public interface IUnblockExpiredUsersService
{
    Task UnblockExpiredUsersAsync(CancellationToken cancellationToken);
}