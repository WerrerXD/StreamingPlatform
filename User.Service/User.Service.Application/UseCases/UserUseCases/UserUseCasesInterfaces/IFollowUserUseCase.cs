namespace User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;

public interface IFollowUserUseCase
{
    Task ExecuteAsync(Guid followerId, Guid followeeId, CancellationToken cancellationToken);
}