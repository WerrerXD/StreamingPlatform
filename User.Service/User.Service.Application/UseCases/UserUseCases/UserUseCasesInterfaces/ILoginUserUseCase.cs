namespace User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces;

public interface ILoginUserUseCase
{
    Task<(string AccessToken, string RefreshToken)> ExecuteAsync(string email, string password, CancellationToken cancellationToken);
}