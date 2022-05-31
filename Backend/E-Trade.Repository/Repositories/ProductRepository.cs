using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_Trade.Repository.Repositories
{
    // Product için oluşturduğumuz custom metodun(interface içindeki) gövdesinin kodladığımız class.

    // ProductRepository class.
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ETradeDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            return await _dbContext.Products.Include(x => x.Category).ToListAsync();
        }

        public async Task<Product> GetProductWithCategory(int id)
        {
            return await _dbContext.Products.Include(x => x.Category).Where(x => x.Id == id).SingleOrDefaultAsync();
        }
    }
}
