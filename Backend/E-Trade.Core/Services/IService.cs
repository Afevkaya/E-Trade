using System.Linq.Expressions;

namespace E_Trade.Core.Services
{
    // Bütün class'lar için temel metodların bulunduğu interface.
    // IGenericRepository interface'ne benzerlikleri bulunur.
    // Ama farklı amaçlar için kullanılır.
    // Repository entity class döner. Service dto class döner. 


    // IService generic interface
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task Update(T entity);
        Task Remove(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task RemoveRange(IEnumerable<T> entities);
    }
}
