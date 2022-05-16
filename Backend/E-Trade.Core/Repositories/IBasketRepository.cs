using E_Trade.Core.Models;

namespace E_Trade.Core.Repositories
{
    public interface IBasketRepository : IGenericRepository<Basket>
    {
        Task<List<Basket>> GetAllAsync();
    }
}
