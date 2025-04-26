namespace Stream.Service.Domain.Interfaces;

public interface IPaymentService
{
    Task<bool> ProcessPaymentAsync(string userId, decimal amount);
}