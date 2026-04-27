using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specifications);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity> specifications);
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

    }
}
