namespace User.Service.Domain.Interfaces;

public interface ILoggingService
{
    Task LogInformationAsync(string message);
    Task LogErrorAsync(string message, Exception exception);
}