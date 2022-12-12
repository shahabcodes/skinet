using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Constants;
using AutoMapper;
using API.Errors;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseApiController
    {
        private IMapper _mapper;
        private IGenericRepository<Product> _productsRepo;
        private IGenericRepository<ProductTypes> _productTypesRepo;
        private IGenericRepository<ProductBrands> _productBrandsRepo;
        
        public string procName= "";

        public ProductsController (IGenericRepository<Product> productsRepo,IGenericRepository
        <ProductTypes> productTypesRepo,IGenericRepository<ProductBrands> productBrandsRepo,
        IMapper mapper)
        {
            _mapper = mapper;
            _productsRepo = productsRepo;
            _productTypesRepo = productTypesRepo;
            _productBrandsRepo = productBrandsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProduct([FromQuery]ProductSpecs specs)                                                   
        {
            procName = StoredPocedures.GetProducts;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("@sort", specs.sort);
            dictionary.Add("@filterByBrand", specs.brandId);
            dictionary.Add("@filterByType", specs.typeId);
            dictionary.Add("@NumberOfRows", specs.rows);
            dictionary.Add("@SearchText", specs.SearchText);
            dictionary.Add("@PageId", specs.PageId);
            var product = await _productsRepo.ListAllAsync(procName, dictionary);
            if (product != null)  { return Ok(_mapper.Map<IReadOnlyList<Product>>(product)); }
            else { return BadRequest(new ApiResponse(400)); }
        }
        
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int Id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Id", Id);
            procName = StoredPocedures.GetProductsById;
            var product = await _productsRepo.GetByIdAsync(procName, parameters);
            if (product == null)  { return NotFound(new ApiResponse(404));  }
            return Ok(_mapper.Map<Product, Product>(product));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductTypes>>> GetProductTypes()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            procName = StoredPocedures.GetProductTypes;
            var types = await _productTypesRepo.ListAllAsync(procName, dictionary);
            if (types != null) { return Ok(types); }
            else { return NotFound(); }
        } 

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrands>>> GetProductBrands()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            procName = StoredPocedures.GetProductBrands;
            var brands = await _productBrandsRepo.ListAllAsync(procName, dictionary);
            if (brands != null)  { return Ok(brands); }
            else { return NotFound(); }
        }  
    }
}