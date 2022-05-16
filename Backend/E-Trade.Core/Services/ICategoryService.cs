using E_Trade.Core.DTOs;
using E_Trade.Core.Models;

namespace E_Trade.Core.Services
{
    // Category için Custom metod(ların) bulunduğu interface 
    // Generic IService interface'ini Category class için implement eder.

    // ICategoryService interface
    public interface ICategoryService : IService<Category>
    {
        //Task<CategoryByIdWithProductsDto> GetSingleCategoryByIdProducts(int id);
        Task<CustomResponseDto<CategoryByIdWithProductsDto>> GetSingleCategoryByIdProducts(int categoryId);
    }
}
