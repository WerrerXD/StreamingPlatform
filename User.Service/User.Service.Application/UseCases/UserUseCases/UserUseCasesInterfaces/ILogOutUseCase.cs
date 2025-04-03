namespace User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;

public interface ILogOutUseCase
{
    Task ExecuteAsync(string accessToken, Guid userId, CancellationToken cancellationToken);
}