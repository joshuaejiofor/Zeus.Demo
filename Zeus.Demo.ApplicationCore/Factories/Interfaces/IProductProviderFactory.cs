using Zeus.Demo.ApplicationCore.Providers.Interfaces;
using Zeus.Demo.Core.Models;

namespace Zeus.Demo.ApplicationCore.Factories.Interfaces
{
    public interface IProductProviderFactory
    {
        IProductProvider Create(ProductType productType);
    }
}
