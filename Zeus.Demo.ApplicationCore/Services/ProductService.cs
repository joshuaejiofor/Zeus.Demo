using AutoMapper;
using Microsoft.Extensions.Configuration;
using Zeus.Demo.ApplicationCore.Services.Interfaces;
using Zeus.Demo.Core.IUnitOfWork;
using Serilog;
using Zeus.Demo.Core.Models;
using Zeus.Demo.Core.Requests;
using Zeus.Demo.ApplicationCore.Factories.Interfaces;

namespace Zeus.Demo.ApplicationCore.Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration, IProductProviderFactory productProviderFactory) 
        : ServiceBase(unitOfWork, mapper, logger, configuration), IProductService
    {
        public async Task ProcessAsync(PurchaseRequest purchaseRequest)
        {
            switch (purchaseRequest.ProductType)
            {
                case ProductType.Flower:
                    await productProviderFactory.Create(purchaseRequest.ProductType).AddToCartAsync(purchaseRequest);
                    break;
                case ProductType.Food:
                case ProductType.Electronic:
                    await productProviderFactory.Create(purchaseRequest.ProductType).CalculateDiscountAsync(purchaseRequest);
                    break;
                default:
                    break;
            }
        }

    }
}