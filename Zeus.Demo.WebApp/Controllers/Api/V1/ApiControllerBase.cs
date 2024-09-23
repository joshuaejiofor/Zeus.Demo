using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zeus.Demo.Core.Helpers.Interfaces;
using Zeus.Demo.Core.IUnitOfWork;
using ILogger = Serilog.ILogger;

namespace Zeus.Demo.WebApp.Controllers.Api.V1
{
    public abstract class ApiControllerBase(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration, IUriHelper uriHelper) : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
        protected readonly IMapper _mapper = mapper;
        protected readonly ILogger _logger = logger;
        protected readonly IConfiguration _configuration = configuration;
        protected readonly IUriHelper _uriHelper = uriHelper;
    }
}
