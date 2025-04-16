using User.Service.Application.Contracts;

namespace User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;

public interface IRegisterUserUseCase
{
    Task ExecuteAsync(RegisterUserRequest request, CancellationToken cancellationToken);
}