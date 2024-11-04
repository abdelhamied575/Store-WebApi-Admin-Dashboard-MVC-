using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Core.Dtos.Products;
using StoreWeb.Core.Helper;
using StoreWeb.Core.Services.Contract;
using StoreWeb.Core.Specifications.Products;
using StoreWebApi.Attributes;
using StoreWebApi.Errors;

namespace StoreWebApi.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [ProducesResponseType(typeof(PaginationResponse<ProductDto>), StatusCodes.Status200OK)]
        [HttpGet] // BaseUrl/api/Products
        [Cashed(100)]
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams productSpec ) // EndPoint
        {


            var result = await _productService.GetAllProductAsync(productSpec);

            //return Ok(new PaginationResponse<ProductDto>(productSpec.PageSize,productSpec.PageIndex,0,result));
            return Ok(result);

        }



        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("brands")] // BaseUrl/api/Products
        //[Route("api/[controller]/Brands")]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands() // EndPoint
        {
            var result = await _productService.GetAllBrandsAsync();

            return Ok(result);

        }

        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("types")] // BaseUrl/api/Products
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes() // EndPoint
        {
            var result = await _productService.GetAllTypesAsync();

            return Ok(result);

        }


        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] // BaseUrl/api/Products/1
        public async Task<ActionResult<ProductDto>> GetProductById(int? id) // EndPoint
        {
            if (id is null) return BadRequest(new ApiErrorResponse(400));

            var result = await _productService.GetProductByIdAsync(id.Value);

            if (result is null) return NotFound(new ApiErrorResponse(404, $"The Product With Id :{id} Not Found At DataBase" ));

            return Ok(result); 

        }



    }
}
