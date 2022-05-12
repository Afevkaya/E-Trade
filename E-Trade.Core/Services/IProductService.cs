using E_Trade.Core.DTOs;
using E_Trade.Core.Models;

namespace E_Trade.Core.Services
{
    // Product için Custom metod(ların) bulunduğu interface 
    // Generic IService interface'ini Product class için implement eder.

    // IProductService interface
    public interface IProductService : IService<Product>
    {
        //Task<List<ProductsWithCategoryDto>> GetProductsWithCategory();
        Task<CustomResponseDto<List<ProductsWithCategoryDto>>> GetProductsWithCategory(); 
        Task<CustomResponseDto<ProductDto>> AddAsyncTwo(ProductDto productDto);
    }
}
