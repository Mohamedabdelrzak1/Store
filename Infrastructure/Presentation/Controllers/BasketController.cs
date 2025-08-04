using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/baskets")]
    public class BasketController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public BasketController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet] // GET api/baskets?id=123
        public async Task<IActionResult> GetBasketById([FromQuery] string id)
        {
            var result = await serviceManager.BasketService.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost] // POST api/baskets
        public async Task<IActionResult> UpdateBasket([FromBody] BasketDto basketDto)
        {
            var result = await serviceManager.BasketService.UpdateBasketAsync(basketDto);
            return Ok(result);
        }

        [HttpDelete] // DELETE api/baskets?id=123
        public async Task<IActionResult> DeleteBasket([FromQuery] string id)
        {
            await serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
