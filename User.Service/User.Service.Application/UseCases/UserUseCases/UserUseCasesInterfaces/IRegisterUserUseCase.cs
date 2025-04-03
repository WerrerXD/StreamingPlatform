namespace User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;

public interface IRegisterUserUseCase
{
    Task ExecuteAsync(string userName, string email, string password, CancellationToken cancellationToken);
}