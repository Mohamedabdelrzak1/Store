using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public PaymentsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("{basketId}")]
        [Authorize]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent(string basketId)
        {
            var result = await _serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(result);
        }
    }  }
