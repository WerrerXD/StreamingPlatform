namespace User.Service.Application.Abstractions;

public interface IDeleteExpiredTokensJob
{
    Task DeleteExpiredTokensAsync(CancellationToken cancellationToken);
}