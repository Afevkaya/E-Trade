using AutoMapper;
using E_Trade.Core.DTOs;
using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using E_Trade.Core.Services;
using E_Trade.Core.UnitOfWorks;

namespace E_Trade.Service.Services
{
    // Product class'ı için yazılmış custom metodların kodlandığı class.
    // Service class'ları kullanıcı tarafı ile etkileşime geçer.
    // Bu yüzden geriye Dto class dönerler.

    // ProductService class.
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IGenericRepository<Product> repository, IProductRepository productRepository, IMapper mapper) : base(unitOfWork, repository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        /*
        public async Task<List<ProductsWithCategoryDto>> GetProductsWithCategory()
        {
            var productsWithCategory = await _productRepository.GetProductsWithCategory();
            var productsWithCategoryDto = _mapper.Map<List<ProductsWithCategoryDto>>(productsWithCategory);
            return productsWithCategoryDto;
        }
        */
        
        public async Task<CustomResponseDto<List<ProductsWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = await _productRepository.GetProductsWithCategory();
            var productsWithCategoryDto = _mapper.Map<List<ProductsWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductsWithCategoryDto>>.Success(200, productsWithCategoryDto); 
        }
        
    }
}
