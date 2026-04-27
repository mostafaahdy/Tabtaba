using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtba.Persistence.Data.DbContexts;

namespace Tabtaba.Persistence.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories=[];

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var EntityType = typeof(TEntity);
            if (_repositories.TryGetValue(EntityType, out object? repository))
            {
                return (IGenericRepository<TEntity>)repository;
            }
            var newRepository = new GenericRepository<TEntity>(_dbContext);
            _repositories.Add(EntityType, newRepository);
            return newRepository;

        }

        public async Task<int> SaveChangesAsync()=> await _dbContext.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

    }
}
