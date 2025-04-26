namespace Stream.Service.BusinessLogic.Abstractions;

public interface ILoggingService
{
    Task LogInformationAsync(string message);
    Task LogErrorAsync(string message, Exception exception);
}