using Stream.Service.BusinessLogic.Abstractions;

namespace Stream.Service.DataAccess;

public class FakePaymentService : IPaymentService
{
    public async Task<bool> ProcessPaymentAsync(string userId, decimal amount)
    {
        await Task.Delay(500);
        
        return true;
    }
}