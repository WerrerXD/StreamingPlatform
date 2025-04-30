using Microsoft.Extensions.Hosting;

namespace Stream.Service.DataAccess.Messaging;

public class RabbitMqBackgroundService : BackgroundService
{
    private readonly RabbitMqEventConsumer _consumer;

    public RabbitMqBackgroundService(RabbitMqEventConsumer consumer)
    {
        _consumer = consumer;
        
        _consumer.Subscribe<UserLoggedInEvent>("user.logged-in", HandleUserLoggedInEvent);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    private Task HandleUserLoggedInEvent(UserLoggedInEvent @event)
    {
        Console.WriteLine($"[Stream Service] Handling UserLoggedIn event: {@event.UserId}");

        return Task.CompletedTask;
    }
}