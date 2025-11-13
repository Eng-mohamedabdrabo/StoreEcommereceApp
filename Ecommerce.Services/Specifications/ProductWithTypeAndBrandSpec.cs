using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.ProductModule;
using Ecommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecommerce.Services.Specifications
{
    public class ProductWithTypeAndBrandSpec : BaseSpecifications<Products, int>

    {
        public ProductWithTypeAndBrandSpec(ProductQueryParams queryParams) :
            base(p => (!queryParams.brandId.HasValue || p.ProductBrandId == queryParams.brandId)
            && (!queryParams.typeId.HasValue || p.ProductTypeId == queryParams.typeId)
            && (string.IsNullOrEmpty(queryParams.search) || p.Name.ToLower().Contains(queryParams.search.ToLower()))
            )


        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch (queryParams.sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(P => P.Price);
                    break;
                default:
                    AddOrderBy(P => P.Id);
                    break;
            }

        }

        public ProductWithTypeAndBrandSpec(int id) : base(X => X.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

    }
}
