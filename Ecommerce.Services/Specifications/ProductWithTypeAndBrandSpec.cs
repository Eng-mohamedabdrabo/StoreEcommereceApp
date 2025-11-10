using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Specifications
{
    public class ProductWithTypeAndBrandSpec<TEntity,TKey> : BaseSpecifications<Products,int>
        where TEntity : BaseEntity<int>
    {
        public ProductWithTypeAndBrandSpec() :base()
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
