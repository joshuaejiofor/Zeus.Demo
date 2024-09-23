using AutoMapper;
using Zeus.Demo.ApplicationCore.Dto;
using Zeus.Demo.Core.Models;

namespace Zeus.Demo.ApplicationCore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();           
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
