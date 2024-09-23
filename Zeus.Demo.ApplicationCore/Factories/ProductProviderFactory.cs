using Microsoft.Extensions.DependencyInjection;
using Zeus.Demo.ApplicationCore.Factories.Interfaces;
using Zeus.Demo.ApplicationCore.Providers;
using Zeus.Demo.ApplicationCore.Providers.Interfaces;
using Zeus.Demo.Core.Models;

namespace Zeus.Demo.ApplicationCore.Factories
{
    public class ProductProviderFactory(IServiceProvider serviceProvider) : IProductProviderFactory
    {
        public IProductProvider Create(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.Electronic:
                    return serviceProvider.GetRequiredService<ElectronicProductProvider>();
                case ProductType.Flower:
                    return serviceProvider.GetRequiredService<FlowerProductProvider>();
                case ProductType.Food:
                    return serviceProvider.GetRequiredService<FoodProductProvider>();

                default:
                    break;
            }

            return null!;
        }
    }
}
