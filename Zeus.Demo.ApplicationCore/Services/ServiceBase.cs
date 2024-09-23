using AutoMapper;
using Microsoft.Extensions.Configuration;
using Zeus.Demo.Core.IUnitOfWork;
using Serilog;

namespace Zeus.Demo.ApplicationCore.Services
{
    public abstract class ServiceBase(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration)
    {
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
        protected readonly IMapper _mapper = mapper;
        protected readonly ILogger _logger = logger;
        protected readonly IConfiguration _configuration = configuration;
    }
}
