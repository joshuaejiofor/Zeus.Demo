using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zeus.Demo.ApplicationCore.Dto;
using Zeus.Demo.Core.Exceptions;
using Zeus.Demo.Core.Helpers.Interfaces;
using Zeus.Demo.Core.Helpers;
using Zeus.Demo.Core.IUnitOfWork;
using Zeus.Demo.Core.Requests.Filters;
using Zeus.Demo.Core.Responses;
using ILogger = Serilog.ILogger;

namespace Zeus.Demo.WebApp.Controllers.Api.V1
{
    [ApiController]
    [Route("api/v1/product")]
    [Produces("application/json")]
    public class ProductApiController(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration, 
        IUriHelper uriHelper) : ApiControllerBase(unitOfWork, mapper, logger, configuration, uriHelper)
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<ProductDto>>))]
        public IActionResult Get([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var result = _unitOfWork.ProductRepository.Pagenate(filter, out int totalRecords, c => true);

            var dtoList = _mapper.Map<List<ProductDto>>(result);

            var pagedReponse = PaginationHelper.CreatePagedReponse(dtoList, validFilter, totalRecords, _uriHelper, route!);
            return Ok(pagedReponse);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(Response<ProductDto>))]
        public async Task<IActionResult> Get(int Id)
        
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(Id);
            var result = _mapper.Map<ProductDto>(product);

            return Ok(new Response<ProductDto>(result));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Response<ProductDto>))]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            var duplicates = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(c => c.Name == productDto.Name && c.ProductType == c.ProductType);
            if (duplicates != null)
                return StatusCode(StatusCodes.Status409Conflict, new Response<ProductDto>(productDto, "Duplicate record", 409, false, 1));

            await _unitOfWork.ProductRepository.AddAsync(productDto);
            await _unitOfWork.CompleteAsync();

            return Ok(new Response<ProductDto>(productDto));
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<ProductDto>))]
        public async Task<IActionResult> Put([FromBody] ProductDto productDto)
        {
            try
            {
                var existingProduct = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(c => c.Id == productDto.Id && c.IsActive) ?? 
                    throw new NotFoundException($"No valid product found with Id = {productDto.Id}");
                _unitOfWork.ProductRepository.Update(productDto);
                await _unitOfWork.CompleteAsync();

                return Ok(new Response<ProductDto>(productDto, 1));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response<ProductDto>(productDto, ex.Message, 404, false, 0));
            }
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(ProductDto))]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var productToDelete = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(c => c.Id == Id && c.IsActive) ??
                     throw new NotFoundException($"No valid product found with Id = {Id}");

                productToDelete.IsActive = false;
                await _unitOfWork.CompleteAsync();

                var productDto = _mapper.Map<ProductDto>(productToDelete);

                return Ok(productDto);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response<ProductDto>(null!, ex.Message, 404, false, 0));
            }
        }
    }
}
