using E_Trade.Core.Models;

namespace E_Trade.Core.Repositories
{
    // Product model class'ına ait özel metodlar bu interface içerisinde tanımlanır.
    // IGenericRepository interface'ini implement eder.

    // IProductRepository interface
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategory();
        Task<Product> GetProductWithCategory(int id);
    }
}
