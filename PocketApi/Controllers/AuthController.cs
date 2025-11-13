using Microsoft.AspNetCore.Mvc;
using PocketApi.Config;
using PocketApi.Interfaces;
using PocketApi.Models;
using PocketApi.Models.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace PocketApi.Controllers;

[Produces("application/json")]
[Route("api/v1/auth")]
[ApiController]
public class AuthController(ILogger<AuthController> logger, IAuthService authService) : Controller
{
    private readonly ILogger<AuthController> _logger = logger;
    private readonly IAuthService _authService = authService;

    [HttpPost("login")]
    [SwaggerOperation(Summary = "Login for a user.", Description = "Login for a user.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(AuthResponseDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            this.HandleModelValidation(_logger);

            AuthResponseDto authResponse = await _authService.LoginAsync(loginDto);

            return Ok(authResponse);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }

    [HttpPost("register")]
    [SwaggerOperation(Summary = "Register for a user.", Description = "Register for a user.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(AuthResponseDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            this.HandleModelValidation(_logger);

            AuthResponseDto authResponse = await _authService.RegisterAsync(registerDto);

            return Ok(authResponse);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }
}