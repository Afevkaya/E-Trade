using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Trade.Repository.Repositories
{
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        private readonly DbSet<Basket> _basketSet;
        public BasketRepository(ETradeDbContext dbContext) : base(dbContext)
        {
            _basketSet = dbContext.Set<Basket>();
        }

        public async Task<List<Basket>> GetAllAsync()
        {
            return await _basketSet.ToListAsync();
        }
    }
}
