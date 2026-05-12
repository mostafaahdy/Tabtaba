using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;

namespace Tabtaba.Services.Specifications
{
    public abstract class BaseSpecifications<TEntity> :ISpecification<TEntity> where TEntity : class
    {
        #region Criteria
        public Expression<Func<TEntity,bool>> Criteria { get; }

        protected BaseSpecifications(Expression<Func<TEntity,bool>> criteriaExpression) => Criteria = criteriaExpression; 

        #endregion

        #region Includes
        public ICollection<Expression<Func<TEntity,object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<TEntity,object>> includeExp)
        {
            IncludeExpressions.Add(includeExp);
        }
        public List<string> IncludeStrings { get; } = new List<string>();

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }


        #endregion

        #region Sorting
        public Expression<Func<TEntity, object>>? OrderdBy { get; private set; }
        public Expression<Func<TEntity, object>>? OrderdByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity,object>> orderByExpression)
        {
            OrderdBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity,object>> orderByDescExpression)
        {
            OrderdByDescending = orderByDescExpression;
        }

        #endregion

        #region Pagination
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set;}


        

        protected void ApplyPagination(int pagesize ,int pageindex)
        {
            
            IsPaginated = true;
            Take = pagesize;
            Skip = pagesize * (pageindex - 1);
        }


        #endregion
    }
}
