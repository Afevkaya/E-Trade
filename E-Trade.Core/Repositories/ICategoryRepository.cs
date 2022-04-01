using E_Trade.Core.Models;

namespace E_Trade.Core.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetSingleCategoryByIdProducts(int id);
    }
}
