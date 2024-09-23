using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Zeus.Demo.Core.IUnitOfWork;
using Zeus.Demo.WebApp.Models;
using ILogger = Serilog.ILogger;

namespace Zeus.Demo.WebApp.Controllers
{
    public class HomeController(IUnitOfWork unitOfWork, ILogger logger, IConfiguration configuration, IMapper mapper) : ControllerShared(unitOfWork, logger, configuration, mapper)
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
