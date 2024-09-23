using Zeus.Demo.Core.Models;

namespace Zeus.Demo.ApplicationCore.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddToCartAsync(Order order);
        Task RemoveFromCartAsync(Order order);
    }
}

