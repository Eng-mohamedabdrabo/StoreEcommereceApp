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
            ISpecifications<TEntity, TKey> specifications)
            where TEntity : BaseEntity<TKey>
        {
            var Query = entryPoint;
            if (specifications is not null)
            {
                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    Query = specifications.IncludeExpressions
                        .Aggregate(Query, (currentQuery, includeExp) => currentQuery
                        .Include(includeExp));
                }
            }
            return Query;
        }
    }
}
