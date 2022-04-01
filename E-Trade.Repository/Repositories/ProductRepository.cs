﻿using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_Trade.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ETradeDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            return await _dbContext.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
