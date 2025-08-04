using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Service.Specifications;
using System.Linq.Expressions;
using Domain.Contracts;

namespace Persistence
{
    public static class SpecificationEvaluation
    {
        public static IQueryable<TEntity> GetQuery<TEntity, TKey>(
            IQueryable<TEntity> inputQuery,
            ISpecifications<TEntity, TKey> spec)
            where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;

            // (WHERE) تطبيق شرط الفلترة 
            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if(spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            // Apply Pagination if enabled
            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }


            //Include تطبيق علاقات 
            query = spec.IncludeExpressions
                      .Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            

            return query;
        }
    }
}
