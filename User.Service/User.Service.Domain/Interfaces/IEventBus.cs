namespace User.Service.Domain.Interfaces;

public interface IEventBus
{
    void Publish<T>(T @event, string routingKey);
}