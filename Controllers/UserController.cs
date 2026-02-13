using cityshop_api.DTO;
using cityshop_api.Helpers;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using cityshop_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cityshop_api.Controllers
{
    public class UserController : BaseController
    {
        private readonly ApplicationDBContext _context;
        private readonly IUserService _userService;
        public UserController(ApplicationDBContext applicationDBContext, IUserService userService)
        {
            _context = applicationDBContext;
            _userService = userService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> UserRegister(RegisterRequest registerRequest)
        {
            try
            {
                var isRegistered = await _userService.UserRegister(registerRequest);

                if (isRegistered)
                {
                    return Ok(ResponseHelper.Success(
                        message: "User registered successfully"
                    ));
                }

                return BadRequest(ResponseHelper.Fail(
                    message: "User registration failed"
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHelper.Fail(
                    message: ex.Message
                ));
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UserLogin(LoginRequest loginRequest)
        {
            try
            {
                var response = await _userService.UserLogin(loginRequest);
                if (response != null)
                {
                    return Ok(ResponseHelper.Success(
                        data: response,
                        message: "User logged in successfully"
                    ));
                }
                return BadRequest(ResponseHelper.Fail(
                    message: "Invalid email or password"
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHelper.Fail(
                    message: ex.Message
                ));
            }
        }
    }
}
