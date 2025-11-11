using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ecommerce.Persistence
{
    public class SpecificationsEvaluator
    {
        public SpecificationsEvaluator() { }

        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(
            IQueryable<TEntity> entryPoint,
            ISpecifications<TEntity, TKey>? specifications)
            where TEntity : BaseEntity<TKey>
        {
            var query = entryPoint;

            if (specifications is not null)
            {
                // Apply Criteria (where condition)
                if (specifications.Criteria is not null)
                {
                    query = query.Where(specifications.Criteria);
                }

                // Apply Include Expressions (eager loading)
                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    query = specifications.IncludeExpressions
                        .Aggregate(query, (currentQuery, includeExp) => currentQuery.Include(includeExp));
                }
            }

            // لازم نرجع الـ query في كل الحالات
            return query;
        }
    }
}
