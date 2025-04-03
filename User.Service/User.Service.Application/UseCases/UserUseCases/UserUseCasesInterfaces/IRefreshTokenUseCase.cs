namespace User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces
{
    public interface IRefreshTokenUseCase
    {
        Task<string> ExecuteAsync(string refreshToken, CancellationToken cancellationToken);
    }
}