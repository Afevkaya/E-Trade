using E_Trade.Core.Models;

namespace E_Trade.Core.Repositories
{
    // Category model class'ına ait özel metodlar bu interface içerisinde tanımlanır.
    // IGenericRepository interface'ini implement eder.
    
    // ICategoryRepository interface
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetSingleCategoryByIdProducts(int id);
    }
}
