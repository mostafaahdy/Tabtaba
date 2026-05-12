using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Persistence.Specifications;
using Tabtba.Persistence.Data.DbContexts;

namespace Tabtaba.Persistence.Repositories
{
    public class GenericRepository<TEntity> :IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);


        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();


        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specifications)
        {
            return await SpecificationsEvaluator<TEntity>.CreateQuery(_dbContext.Set<TEntity>(),specifications).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public async Task<TEntity?> GetByIdAsync(params object[] keyValues)
        {
            
            return await _context.Set<TEntity>().FindAsync(keyValues);
        }
        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity,bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity> specifications)
        {
            return await SpecificationsEvaluator<TEntity>.CreateQuery(_dbContext.Set<TEntity>(),specifications).FirstOrDefaultAsync();
        }
        private readonly ApplicationDbContext _context;



        public async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec)
        {
            
            return await this.ApplySpecification(spec).ToListAsync();
        }

        public void Remove(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);
        
        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);
        public IQueryable<TEntity> GetQueryable()
        {
            return _dbContext.Set<TEntity>().AsNoTracking(); 
        }

        

        #region Helper Method
        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationsEvaluator<TEntity>.CreateQuery(_dbContext.Set<TEntity>().AsQueryable(),spec);
        } 
        #endregion
    }
}
