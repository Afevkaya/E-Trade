using E_Trade.Core.Repositories;
using E_Trade.Core.Services;
using E_Trade.Core.UnitOfWorks;
using E_Trade.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Trade.Service.Services
{
    // Bütün entity class'lar için temel işlemleri(metodları) içeren class.
    // Entityler için custom metod kodlanılan classlar bu classı inherit eder.

    // Service generic class
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }
        
        public async Task<T> GetByIdAsync(int id)
        {
            var hasProduct = await _repository.GetByIdAsync(id);
            if (hasProduct == null)
            {
                throw new NotFoundException($"{typeof(T).Name}({id}) not found");
            }
            return await _repository.GetByIdAsync(id);
        }

        public async Task Remove(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
           
        }

        public async Task RemoveRange(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task Update(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
