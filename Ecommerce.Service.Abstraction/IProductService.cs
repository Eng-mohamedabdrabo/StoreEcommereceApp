using Ecommerce.Shared;
using Ecommerce.Shared.ProductDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Service.Abstraction
{
    public interface IProductService
    {
        /// <summary>
        /// Get all products with filtering, searching, and sorting options.
        /// </summary>
        Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams);

        /// <summary>
        /// Get a single product with its brand and type details.
        /// </summary>
        Task<ProductDTO?> GetProductByIdAsync(int id);

        /// <summary>
        /// Get all available product brands.
        /// </summary>
        Task<IEnumerable<BrandsDTO>> GetBrandsAsync();

        /// <summary>
        /// Get all available product types.
        /// </summary>
        Task<IEnumerable<TypesDTO>> GetTypesAsync();
    }
}
