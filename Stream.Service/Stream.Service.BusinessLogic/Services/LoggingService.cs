using Stream.Service.BusinessLogic.Abstractions;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.BusinessLogic.Services;

public class LoggingService : ILoggingService
{
    private readonly IElasticsearchService _logger;

    public LoggingService(IElasticsearchService logger)
    {
        _logger = logger;
    }

    public async Task LogInformationAsync(string message)
    {
        await _logger.LogAsync(message, "Information");
    }

    public async Task LogErrorAsync(string message, Exception exception)
    {
        await _logger.LogAsync(message, "Error", exception);
    }
}