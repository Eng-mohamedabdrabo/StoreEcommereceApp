using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria{ get; }
        protected BaseSpecifications(Expression<Func<TEntity, bool>> criteriaExp)
        {
            Criteria = criteriaExp;
        }

        protected void AddInclude(Expression<Func<TEntity,object>> includeExp)
        { 
            IncludeExpressions.Add(includeExp);
        }
    }
}
