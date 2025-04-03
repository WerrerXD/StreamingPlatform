namespace User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;

public interface IBlockUserUseCase
{
    Task ExecuteAsync(Guid userId, int daysBanned, CancellationToken cancellationToken);
}