using AutoMapper;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities.ProductModule;
using Ecommerce.Service.Abstraction;
using Ecommerce.Services.Specifications;
using Ecommerce.Shared;
using Ecommerce.Shared.ProductDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class ProductsService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ✅ Get all products (filtering + sorting + pagination)
        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var spec = new ProductWithTypeAndBrandSpec(queryParams);

            // Get paginated data
            var products = await _unitOfWork
                .GetRepository<Products, int>()
                .GetAllAsync(spec);

            // Map to DTO
            var mappedData = _mapper.Map<IEnumerable<ProductDTO>>(products);

            // Get total count for pagination
            var totalItems = await _unitOfWork
                .GetRepository<Products, int>()
                .CountAsync(new ProductWithFiltersForCountSpec(queryParams));

            return new PaginatedResult<ProductDTO>(
                queryParams.PageIndex,
                queryParams.PageSize,
                totalItems,
                mappedData
            );
        }

        // ✅ Get product by id (with includes)
        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithTypeAndBrandSpec(id);

            var product = await _unitOfWork
                .GetRepository<Products, int>()
                .GetByIdAsync(spec);

            return _mapper.Map<ProductDTO>(product);
        }

        // ✅ Get all brands
        public async Task<IEnumerable<BrandsDTO>> GetBrandsAsync()
        {
            var brands = await _unitOfWork
                .GetRepository<ProductBrand, int>()
                .GetAllAsync();

            return _mapper.Map<IEnumerable<BrandsDTO>>(brands);
        }

        // ✅ Get all types
        public async Task<IEnumerable<TypesDTO>> GetTypesAsync()
        {
            var types = await _unitOfWork
                .GetRepository<ProductType, int>()
                .GetAllAsync();

            return _mapper.Map<IEnumerable<TypesDTO>>(types);
        }
    }
}
