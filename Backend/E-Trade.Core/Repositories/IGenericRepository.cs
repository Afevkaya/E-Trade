using System.Linq.Expressions;

namespace E_Trade.Core.Repositories
{
    // Bütün class'lara ait temel olması gereken metodların bulunduğu interface

    // IGenericRepository interface
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void RemoveRange(IEnumerable<TEntity> entities);

    }
}
