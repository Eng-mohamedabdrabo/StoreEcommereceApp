using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.ProductModule;
using Ecommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Specifications
{
    public class ProductWithTypeAndBrandSpec : BaseSpecifications<Products,int>
        
    {
        public ProductWithTypeAndBrandSpec(ProductQueryParams queryParams) :
            base(p=>(!queryParams.brandId.HasValue || p.ProductBrandId==queryParams.brandId)
            &&(!queryParams.brandId.HasValue||p.ProductTypeId== queryParams.brandId)
            &&(string.IsNullOrEmpty(queryParams.search) || p.Name.ToLower().Contains(queryParams.search))
            )
            
            
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
