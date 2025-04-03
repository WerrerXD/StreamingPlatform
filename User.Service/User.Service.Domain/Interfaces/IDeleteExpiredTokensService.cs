namespace User.Service.Domain.Interfaces;

public interface IDeleteExpiredTokensService
{
    Task DeleteExpiredTokens(CancellationToken cancellationToken);
}