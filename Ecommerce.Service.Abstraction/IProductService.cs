using Ecommerce.Shared.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync(int?brandId,int?typeId);
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandsDTO>> GetBrandsAsync();
        Task<IEnumerable<TypesDTO>>  GetTypesAsync();

    }
}
