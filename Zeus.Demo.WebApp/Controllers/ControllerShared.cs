using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zeus.Demo.Core.IUnitOfWork;
using ILogger = Serilog.ILogger;

namespace Zeus.Demo.WebApp.Controllers
{
    public abstract class ControllerShared(IUnitOfWork unitOfWork, ILogger logger, IConfiguration configuration, IMapper mapper)
       : Controller
    {
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
        protected readonly ILogger _logger = logger;
        protected readonly IConfiguration _configuration = configuration;
        protected readonly IMapper _mapper = mapper;
    }
}
