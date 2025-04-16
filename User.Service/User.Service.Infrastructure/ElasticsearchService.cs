using Elastic.Clients.Elasticsearch;
using User.Service.Domain.Interfaces;

namespace User.Service.Infrastructure;

public class ElasticsearchService : IElasticsearchService
{
    private readonly ElasticsearchClient _client;

    public ElasticsearchService(string elasticsearchUrl)
    {
        var settings = new ElasticsearchClientSettings(new Uri(elasticsearchUrl))
            .DefaultIndex("app-logs");

        _client = new ElasticsearchClient(settings);
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