using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities.ProductModule
{
    public class Products : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }

        #region Relations
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        #endregion
    }
}
