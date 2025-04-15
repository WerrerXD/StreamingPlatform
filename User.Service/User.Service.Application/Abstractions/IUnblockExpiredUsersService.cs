namespace User.Service.Application.Abstractions;

public interface IUnblockExpiredUsersService
{
    Task UnblockExpiredUsersAsync(CancellationToken cancellationToken);
}