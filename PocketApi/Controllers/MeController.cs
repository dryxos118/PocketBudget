using Microsoft.AspNetCore.Mvc;
using PocketApi.Config;
using PocketApi.Interfaces;
using PocketApi.Models;
using PocketApi.Models.Dto.User;
using Swashbuckle.AspNetCore.Annotations;

namespace PocketApi.Controllers;

[Produces("application/json")]
[Route("api/v1/me")]
[ApiController]
public class MeController(ILogger<MeController> logger, IUserService userService) : Controller
{
    private readonly ILogger<MeController> _logger = logger;
    private readonly IUserService _userService = userService;

    [HttpGet("info")]
    [SwaggerOperation(Summary = "Get Me", Description = "Get Me")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(UserSummaryDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> GetInfo()
    {
        try
        {
            int userId = this.GetUserId();

            UserSummaryDto userSummary = await _userService.GetUserSummaryAsync(userId);

            return Ok(userSummary);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }

    [HttpGet("settings")]
    [SwaggerOperation(Summary = "Get Settings", Description = "Get Settings")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(UserSettingsDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> GetSettings()
    {
        try
        {
            int userId = this.GetUserId();

            UserSettingsDto userSettings = await _userService.GetUserSettingAsync(userId);

            return Ok(userSettings);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }

    // TODO
    [HttpPost("update/me")]
    [SwaggerOperation(Summary = "Update Me", Description = "Update Me")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(bool))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> UpdateMe([FromBody] UserSummaryDto userSummaryDto)
    {
        throw new NotImplementedException();
    }

    // TODO
    [HttpPost("update/settings")]
    [SwaggerOperation(Summary = "Update Settings", Description = "Update Settings")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(bool))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> UpdateSettings([FromBody] UserSettingsDto userSettingsDto)
    {
        throw new NotImplementedException();
    }
}