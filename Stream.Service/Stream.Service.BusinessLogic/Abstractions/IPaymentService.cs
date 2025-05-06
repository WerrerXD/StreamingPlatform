namespace Stream.Service.BusinessLogic.Abstractions;

public interface IPaymentService
{
    Task<bool> ProcessPaymentAsync(string userId, decimal amount);
}