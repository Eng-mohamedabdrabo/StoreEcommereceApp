using Ecommerce.Domain.Entities.ProductModule;
using Ecommerce.Shared;
using Ecommerce.Shared.ProductDTOs;

namespace Ecommerce.Services.Specifications
{
    public class ProductWithFiltersForCountSpec : BaseSpecifications<Products, int>
    {
        public ProductWithFiltersForCountSpec(ProductQueryParams queryParams)
            : base(x =>
                (string.IsNullOrEmpty(queryParams.search)
                    || x.Name.ToLower().Contains(queryParams.search.ToLower())) &&

                (!queryParams.brandId.HasValue
                    || x.ProductBrandId == queryParams.brandId) &&

                (!queryParams.typeId.HasValue
                    || x.ProductTypeId == queryParams.typeId)
            )
        {
            // لا Includes
            // لا OrderBy
            // لا Pagination
        }
    }
}
