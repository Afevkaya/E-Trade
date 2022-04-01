using E_Trade.Core.DTOs;
using E_Trade.Core.Models;

namespace E_Trade.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<List<ProductsWithCategoryDto>> GetProductsWithCategory();
    }
}
