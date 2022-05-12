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
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork, IGenericRepository<Product> repository, IProductRepository productRepository, IMapper mapper) : base(unitOfWork, repository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto<ProductDto>> AddAsyncTwo(ProductDto productDto)
        {

            Product product;
            if(productDto.Id > 0)
            {
                var products = _productRepository.GetAll();
                var oldProduct = products.FirstOrDefault(p => p.Id == productDto.Id);
                product = oldProduct;
                product.StockQuantity = productDto.Quantity;
                _productRepository.Update(product);
            }
            else
            {
                product = _mapper.Map<Product>(productDto);
                await _productRepository.AddAsync(product);
            }
            
            await _unitOfWork.CommitAsync();

            var newProductDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Quantity = product.StockQuantity,
                CategoryId = product.CategoryId,
            };
            return CustomResponseDto<ProductDto>.Success(200, newProductDto);
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
