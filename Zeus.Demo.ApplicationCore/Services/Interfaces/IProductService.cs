using Zeus.Demo.Core.Requests;

namespace Zeus.Demo.ApplicationCore.Services.Interfaces
{
    public interface IProductService
    {
        Task ProcessAsync(PurchaseRequest purchaseRequest);
    }
}
