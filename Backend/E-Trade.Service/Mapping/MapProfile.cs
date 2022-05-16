using AutoMapper;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;

namespace E_Trade.Service.Mapping
{
    // Entity classlarının Dto classlarına ya da Dto classlarının Entity classlarına maplendiği class.
    // Db tarafından gelen ya da Db tarafına yansıyacak class Entity class.
    // Kullanıcıya gösterilecek veya kullanıcıdan gelecek class Dto class.
    // Bu yüzden mapleme işlemi yapılır.
    // AutoMapper pakedini yüklemek gerekli.
    // Profile class'ını inherit edilmeli.

    // MapProfile class
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductsWithCategoryDto>();
            CreateMap<Category, CategoryByIdWithProductsDto>();
            CreateMap<AppUser, AppUserDto>().ReverseMap();
        }
    }
}
