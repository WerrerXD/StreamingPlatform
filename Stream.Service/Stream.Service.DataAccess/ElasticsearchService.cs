using Elastic.Clients.Elasticsearch;
using Stream.Service.BusinessLogic.Abstractions;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.DataAccess;

public class ElasticsearchService : IElasticsearchService
{
    private readonly ElasticsearchClient _client;

    public ElasticsearchService(ElasticsearchClient client)
    {
        _client = client;
    }

    public async Task LogAsync(string message, string level, Exception? exception = null)
    {
        var logEntry = new
        {
            @timestamp = DateTime.UtcNow,
            message,
            level,
            exception = exception?.ToString()
        };

        var response = await _client.IndexAsync(logEntry);

        if (!response.IsValidResponse)
        {
            throw new Exception($"Failed to log to Elasticsearch: {response.DebugInformation}");
        }
    }
}