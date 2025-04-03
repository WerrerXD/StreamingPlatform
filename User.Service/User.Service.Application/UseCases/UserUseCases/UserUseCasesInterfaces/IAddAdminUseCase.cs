namespace User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;

public interface IAddAdminUseCase
{
    Task ExecuteAsync(Guid userId, CancellationToken cancellationToken);
}