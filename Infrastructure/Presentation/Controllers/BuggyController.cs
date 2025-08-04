using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Shared.Dtos.ErrorModels;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        /// <summary>
        /// Returns 404 Not Found for testing Frontend error handling.
        /// </summary>
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(new ErrorDetails
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = "The requested resource was not found."
            });
        }

        /// <summary>
        /// Returns 500 Internal Server Error for testing Frontend error handling.
        /// </summary>
        [HttpGet("servererror")]
        public IActionResult GetServerErrorRequest()
        {
            try
            {
                object obj = null;
                var res = obj.ToString(); // Will throw NullReferenceException
                return Ok(res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDetails
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = "An internal server error occurred. Please try again later."
                });
            }
        }

        /// <summary>
        /// Returns 400 Bad Request for testing Frontend error handling.
        /// </summary>
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ErrorDetails
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "Bad request. Please check your input and try again."
            });
        }

        /// <summary>
        /// Returns 400 Bad Request with validation error for testing Frontend error handling.
        /// </summary>
        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ErrorDetails
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Id must be greater than zero."
                });
            }

            return Ok(new { Message = $"Request received successfully with Id: {id}" });
        }

        /// <summary>
        /// Returns 401 Unauthorized for testing Frontend error handling.
        /// </summary>
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorizedRequest()
        {
            return Unauthorized(new ErrorDetails
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                ErrorMessage = "You are not authorized to access this resource."
            });
        }
    }
}
