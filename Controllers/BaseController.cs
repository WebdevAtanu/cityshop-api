using cityshop_api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace cityshop_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        // Helper property to get UserId from the JWT "sub" claim
        protected string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)
                                    ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        // Helper property to get Email from JWT
        protected string? Email => User.FindFirstValue(ClaimTypes.Email)
                                ?? User.FindFirstValue(JwtRegisteredClaimNames.Email);

        // custom claim
        //protected string? ShopId => User.FindFirstValue("shopId");

        // Helper method to return a standardized success response with data
        protected IActionResult Success<T>(T data, string message = "Request successful")
        {
            var response = ResponseHelper.ResponseWithData(data, message);
            return StatusCode(response.StatusCode, response);
        }

        // Helper method to return a standardized success response without data
        protected IActionResult Success(string message = "Success")
        {
            var response = ResponseHelper.ResponseWithoutData(message);
            return StatusCode(response.StatusCode, response);
        }

        // Helper method to return a standardized failure response
        protected IActionResult Fail(string message)
        {
            var response = ResponseHelper.ResponseFailed(message);
            return StatusCode(response.StatusCode, response);
        }
    }
}
