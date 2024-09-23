using AutoMapper;
using Microsoft.Extensions.Configuration;
using Serilog;
using Zeus.Demo.ApplicationCore.Providers.Interfaces;
using Zeus.Demo.Core.IUnitOfWork;
using Zeus.Demo.Core.Requests;

namespace Zeus.Demo.ApplicationCore.Providers
{
    public class FlowerProductProvider(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration) : ProductProviderBase(unitOfWork, mapper, logger, configuration), IProductProvider
    {
        public Task CalculateDiscountAsync(PurchaseRequest purchaseRequest)
        {
            throw new NotImplementedException();
        }
    }
}
