using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using User.Service.Domain.Interfaces;

namespace User.Service.Infrastructure.Messaging;

public class RabbitMqEventBus : IEventBus, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqEventBus(string hostName = "localhost")
    {
        var factory = new ConnectionFactory { HostName = hostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        
        _channel.ExchangeDeclare(exchange: "user-stream-exchange", type: ExchangeType.Topic);
    }

    public void Publish<T>(T @event, string routingKey)
    {
        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "user-stream-exchange",
            routingKey: routingKey,
            basicProperties: null,
            body: body);

        Console.WriteLine($"[Producer] Sent event: {routingKey}, Data: {message}");
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}