using Microsoft.AspNetCore.Mvc;
using PocketApi.Config;
using PocketApi.Interfaces;
using PocketApi.Models;
using PocketApi.Models.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace PocketApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController(ILogger<AuthController> logger, IPocketAuthService pocketAuthService) : Controller
    {
        private readonly ILogger<AuthController> _logger = logger;
        private readonly IPocketAuthService _pocketAuthService = pocketAuthService;

        [HttpPost("register")]
        [SwaggerOperation(
            Summary = "Register a new user",
            Description = "Registers a new user in the system."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns AuthResponseDto if the user was successfully registered.", typeof(AuthResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request. The provided user login data is invalid.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error. An unexpected error occurred.")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                this.HandleModelValidation(_logger);

                AuthResponseDto response = await _pocketAuthService.Register(registerDto);

                if (response == null)
                {
                    throw new PocketActionResult("User registration failed.", ErrorType.BadRequest);
                }

                return Ok(response);
            }
            catch (PocketActionResult ex)
            {
                return this.HandlePocketActionResult(ex, _logger);
            }
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Authenticate a user",
            Description = "Authenticates a user and returns a AuthResponseDto if the credentials are valid."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a AuthResponseDto for the authenticated user.", typeof(AuthResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request. The provided user login data is invalid.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error. An unexpected error occurred.")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                this.HandleModelValidation(_logger);

                AuthResponseDto response = await _pocketAuthService.Login(loginDto);

                if (response == null)
                {
                    throw new PocketActionResult("Invalid credentials.", ErrorType.BadRequest);
                }

                return Ok(response);
            }
            catch (PocketActionResult ex)
            {
                return this.HandlePocketActionResult(ex, _logger);
            }
        }

        [HttpPost("/[action]")]
        public async Task<IActionResult> RefreshToken(int userId)
        {
            await Task.Delay(1);

            return Ok($"Hello World : {userId}");
        }
    }
}
