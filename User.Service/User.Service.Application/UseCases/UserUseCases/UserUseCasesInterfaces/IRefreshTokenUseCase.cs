namespace User.Service.Application.UseCases.UserUseCases.UserUseCasesInterfaces
{
    public interface IRefreshTokenUseCase
    {
        Task<(string AccessToken, string RefreshToken)> ExecuteAsync(string refreshToken, CancellationToken cancellationToken);
    }
}