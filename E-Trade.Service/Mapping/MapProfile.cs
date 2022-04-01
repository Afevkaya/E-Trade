using AutoMapper;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;

namespace E_Trade.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductsWithCategoryDto>();
        }
    }
}
