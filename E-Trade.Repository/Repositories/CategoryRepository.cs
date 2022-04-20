using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_Trade.Repository.Repositories
{
    // Category için oluşturduğumuz custom metodun(interface içindeki) gövdesinin kodladığımız class.

    // CategoryRepository class.
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ETradeDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> GetSingleCategoryByIdProducts(int categoryId)
        {

            return await _dbContext.Categories.Include(x => x.Products).Where(x => x.Id == categoryId).SingleOrDefaultAsync();
        }
    }
}
