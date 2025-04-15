namespace User.Service.Application.Abstractions;

public interface IDeleteExpiredTokensService
{
    Task DeleteExpiredTokensAsync(CancellationToken cancellationToken);
}