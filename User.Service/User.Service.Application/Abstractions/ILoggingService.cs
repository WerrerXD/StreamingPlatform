namespace User.Service.Application.Abstractions;

public interface ILoggingService
{
    Task LogInformationAsync(string message);
    Task LogErrorAsync(string message, Exception exception);
}