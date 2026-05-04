using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tabtaba.Persistence.Specifications
{
    internal static class SpecificationsEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> CreateQuery(IQueryable<TEntity> Entrypoint,
            ISpecification<TEntity> specification)
        {
                var Query = Entrypoint;
            if( specification is not null )
            {
                if (specification.Criteria != null)
                {
                    Query = Query.Where(specification.Criteria);
                }
                if ( specification.IncludeExpressions is not null && specification.IncludeExpressions.Any() )
                {
                    Query = specification.IncludeExpressions.Aggregate(Query,
                        (currentQuery,includeExp) => currentQuery.Include(includeExp));
                }
                if( specification.OrderdBy != null )
                {
                    Query = Query.OrderBy(specification.OrderdBy);
                }
                if( specification.OrderdByDescending != null )
                {
                    Query = Query.OrderByDescending(specification.OrderdByDescending);
                }
                if( specification.IsPaginated )
                {
                    Query = Query.Skip(specification.Skip).Take(specification.Take);
                }
            }
            return Query;
        }
    }
}
