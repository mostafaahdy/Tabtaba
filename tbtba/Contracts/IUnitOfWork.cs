using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Contracts
{
    public interface IUnitOfWork :IDisposable
    {
        Task<int> SaveChangesAsync();
        ValueTask DisposeAsync();
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity: class;
    }
}

