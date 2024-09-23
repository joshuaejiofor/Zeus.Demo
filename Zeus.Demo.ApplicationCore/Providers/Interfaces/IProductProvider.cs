using Zeus.Demo.Core.Requests;

namespace Zeus.Demo.ApplicationCore.Providers.Interfaces
{
    public interface IProductProvider
    {
        Task CalculateDiscountAsync(PurchaseRequest purchaseRequest);
        Task AddToCartAsync(PurchaseRequest purchaseRequest);
    }
}
