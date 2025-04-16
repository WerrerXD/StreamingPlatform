namespace User.Service.Domain.Interfaces;

public interface IElasticsearchService
{
    Task LogAsync(string message, string level, Exception? exception = null);
}