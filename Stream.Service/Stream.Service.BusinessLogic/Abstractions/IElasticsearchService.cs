namespace Stream.Service.BusinessLogic.Abstractions;

public interface IElasticsearchService
{
    Task LogAsync(string message, string level, Exception? exception = null);
}