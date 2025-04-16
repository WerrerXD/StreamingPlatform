namespace User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;

public interface IUnfollowUserUseCase
{
    Task ExecuteAsync(Guid followerId, Guid followeeId, CancellationToken cancellationToken);
}