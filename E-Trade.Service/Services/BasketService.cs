using E_Trade.Core.Models;
using E_Trade.Core.Repositories;
using E_Trade.Core.Services;
using E_Trade.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Trade.Service.Services
{
    public class BasketService : Service<Basket>, IBasketService
    {
        public BasketService(IUnitOfWork unitOfWork, IGenericRepository<Basket> repository) : base(unitOfWork, repository)
        {
        }
    }
}
