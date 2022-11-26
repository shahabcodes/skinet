using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Core.Entities;
using Dapper;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _context;

        public ProductsController (IProductRepository context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProduct()
        {
            var product = await _context.GetProductsAsync();
            return Ok(product);
        }
        

        [HttpGet("{Id}")]
        public async Task<ActionResult<Product>> GetProduct(int Id)
        {
            return await _context.GetProductByIdAsync(Id);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrands>>> GetProductBrands()
        {
            var brands = await _context.GetProductBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductTypes>>> GetProductTypes()
        {
            var types = await _context.GetProductTypesAsync();
            return Ok(types);
        }              
    }
}