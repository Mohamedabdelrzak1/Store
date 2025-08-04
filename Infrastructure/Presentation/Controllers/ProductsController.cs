using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using ServiceAbstraction;
using Shared;
using Shared.Dto;
using Shared.Dtos.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager ) : ControllerBase
    {
      // EndPoint : Public non-Static Method

        // Sort : NameAsec [default]
        // Sort : NameDesec
        // Sort : PriceAsec
        // Sort : PriceDesec

        [HttpGet] // Get : /api/Products
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [Cache(100)]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecificationsprameter Specprameter)
        {
            var result = await serviceManager.ProductService.GetAllProductsAsync(Specprameter);
            return Ok(result); // 200

        }

        [HttpGet("{id}")]  // Endpoint: GET api/Products/5
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        
        public async Task<ActionResult<ProductDto>> GetAllProductById(int id)
        {
            var result = await serviceManager.ProductService.GetProductByIdAsync(id);

            if (result == null)
                return NotFound($"Product with ID {id} not found."); // 404

            return Ok(result); // 200
        }


        [HttpGet("brands")]  // Get : /api/Products/brands
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandDto>> GetAllBrands()
        {
            var result = await serviceManager.ProductService.GetAllBrandsAsync();

            if (result == null || !result.Any())
                return NotFound(); // 404 أو BadRequest حسب ما تحب

            return Ok(result); // 200

        }

        [HttpGet("types")]  // Get : /api/Products/types
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<TypeDto>> GetAllTypes()
        {
            var result = await serviceManager.ProductService.GetAllTypesAsync();

            if (result == null || !result.Any())
                return NotFound(); // 404 أو BadRequest حسب ما تحب

            return Ok(result); // 200

        }
    }
}
