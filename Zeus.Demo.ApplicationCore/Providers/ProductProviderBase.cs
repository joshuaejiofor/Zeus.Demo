using AutoMapper;
using Microsoft.Extensions.Configuration;
using Serilog;
using Zeus.Demo.Core.IUnitOfWork;
using Zeus.Demo.Core.Requests;

namespace Zeus.Demo.ApplicationCore.Providers
{
    public abstract class ProductProviderBase(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration)
    {
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
        protected readonly IMapper _mapper = mapper;
        protected readonly ILogger _logger = logger;
        protected readonly IConfiguration _configuration = configuration;

        public Task AddToCartAsync(PurchaseRequest purchaseRequest)
        {
            throw new NotImplementedException();
        }
    }
}
