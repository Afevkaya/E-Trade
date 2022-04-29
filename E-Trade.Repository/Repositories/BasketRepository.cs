using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Trade.Repository.Repositories
{
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        public BasketRepository(ETradeDbContext dbContext) : base(dbContext)
        {
        }
    }
}
