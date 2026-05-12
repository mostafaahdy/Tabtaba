using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specifications);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetByIdAsync(params object[] keyValues);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity> specifications);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity,bool>> predicate);
        Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec);
        Task AddAsync(TEntity entity);
        IQueryable<TEntity> GetQueryable();

        void Update(TEntity entity);

        void Remove(TEntity entity);

    }
}
