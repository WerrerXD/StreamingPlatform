using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Stream.Service.DataAccess.Messaging;

public class RabbitMqEventConsumer : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly Dictionary<Guid, Guid> _loggedUsers = new();

    public RabbitMqEventConsumer(string hostName = "localhost")
    {
        var factory = new ConnectionFactory { HostName = hostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: "user-stream-exchange", type: ExchangeType.Topic);
    }

    public void Subscribe<T>(string routingKey, Func<T, Task> handler)
    {
        var queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: queueName, exchange: "user-stream-exchange", routingKey: routingKey);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"[Consumer] Received event: {routingKey}, Data: {message}");

            try
            {
                var @event = JsonSerializer.Deserialize<T>(message);
                await handler(@event);
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Consumer] Error processing event: {ex.Message}");
                _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
            }
        };

        _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
    }

    private Task HandleUserLoggedInEvent(UserLoggedInEvent @event)
    {
        Console.WriteLine($"[Stream Service] Handling UserLoggedIn event: {@event.UserId}");
        
        _loggedUsers[@event.UserId] = @event.UserId;

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}