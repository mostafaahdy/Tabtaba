using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Contracts
{
    public interface ISpecification<TEntity>
    { 
        #region Criteria
		 public Expression<Func<TEntity,bool>> Criteria { get; } 

         #endregion

        #region Includes

        public ICollection<Expression<Func<TEntity,object>>> IncludeExpressions { get; }       
        public List<string> IncludeStrings { get; }
        #endregion

        #region Sorting 

        public Expression<Func<TEntity,object>> OrderdBy {get;}
       public Expression<Func<TEntity,object>> OrderdByDescending {get; }

        #endregion

        #region Pagination
        public int Take { get; }
        public int Skip { get; }
        public bool IsPaginated { get; }
        #endregion
    }
}
