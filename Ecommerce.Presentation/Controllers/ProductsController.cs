using Ecommerce.Service.Abstraction;
using Ecommerce.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductQueryParams queryParams) 
        {
            var products = await _productService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetProductBrands()
        {
            var brands = await _productService.GetBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypes()
        {
            var types = await _productService.GetTypesAsync();
            return Ok(types);
        }
    }
}
