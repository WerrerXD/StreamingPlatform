namespace User.Service.Application.Abstractions;

public interface IUnblockExpiredUsersJob
{
    Task UnblockExpiredUsersAsync(CancellationToken cancellationToken);
}