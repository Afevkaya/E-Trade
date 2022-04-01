using E_Trade.Core.DTOs;
using E_Trade.Core.Models;

namespace E_Trade.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        Task<CategoryByIdWithProductsDto> GetSingleCategoryByIdProducts(int id);
    }
}
