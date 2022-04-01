using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Trade.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ETradeDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> GetSingleCategoryByIdProducts(int categoryId)
        {
           return await _dbContext.Categories.Include(x=>x.Products).Where(x=>x.Id == categoryId).SingleOrDefaultAsync();
        }
    }
}
