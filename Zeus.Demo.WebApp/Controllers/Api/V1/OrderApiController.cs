using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zeus.Demo.ApplicationCore.Services.Interfaces;
using Zeus.Demo.Core.Helpers.Interfaces;
using Zeus.Demo.Core.IUnitOfWork;
using Zeus.Demo.Core.Models;
using ILogger = Serilog.ILogger;

namespace Zeus.Demo.WebApp.Controllers.Api.V1
{
    [ApiController]
    [Route("api/v1/order")]
    [Produces("application/json")]
    public class OrderApiController(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration, 
        IUriHelper uriHelper, IOrderService orderService) : ApiControllerBase(unitOfWork, mapper, logger, configuration, uriHelper)
    {
        [HttpPost("addtocart")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult AddToCart(Order order)
        {
            try
            {
                orderService.AddToCartAsync(order);
                return Ok();
            }
            catch (Exception ex)
            {
                var problem = ex.InnerException?.ToString() ?? ex.Message.ToString();
                return BadRequest(problem);
            }
        }

        [HttpPost("removefromcart")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult RemoveFromCart(Order order)
        {
            try
            {
                orderService.RemoveFromCartAsync(order);
                return Ok();
            }
            catch (Exception ex)
            {
                var problem = ex.InnerException?.ToString() ?? ex.Message.ToString();
                return BadRequest(problem);
            }
        }
    }
}
