using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

            if(spec.OrderBy is not null) 
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if(spec.OrderByDesending is not null)
            {
                query= query.OrderByDescending(spec.OrderByDesending);
            }

            query = spec.Includes.Aggregate(query, (currentQuery, includesExpression) => currentQuery.Include(includesExpression));

            return query;
        }
    }
}
