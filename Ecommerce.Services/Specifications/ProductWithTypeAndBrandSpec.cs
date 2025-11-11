using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Specifications
{
    public class ProductWithTypeAndBrandSpec : BaseSpecifications<Products,int>
        
    {
        public ProductWithTypeAndBrandSpec() :base(null)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

        public ProductWithTypeAndBrandSpec(int id) : base(X=>X.Id==id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
