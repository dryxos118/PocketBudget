using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PocketApi.Config;
using PocketApi.Interfaces;
using PocketApi.Models;
using PocketApi.Models.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace PocketApi.Controllers;

[Produces("application/json")]
[Route("api/v1/portal")]
[ApiController]
public class PortalController(
    ILogger<PortalController> logger,
    IProfileService profileService,
    IReportingService reportingService) : Controller
{
    private readonly ILogger<PortalController> _logger = logger;
    private readonly IProfileService _profileService = profileService;
    private readonly IReportingService _reportingService = reportingService;

    [HttpGet("profile")]
    [SwaggerOperation(description: "Get profile info")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns ProfileInfoDto", typeof(ProfileInfoDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Profile info not found")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authenticated")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
    public async Task<IActionResult> GetProfile()
    {
        int userId = this.GetUserId();
        try
        {
            ProfileInfoDto profileInfo = await _profileService.GetProfileInfoAsync(userId);
            return Ok(profileInfo);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }

    [HttpGet("report/monthly")]
    [SwaggerOperation(description: "Get monthly report")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns double", typeof(double))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Monthly report not found")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authenticated")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
    public async Task<IActionResult> GetMonthlyReport(int year, int month)
    {
        int userId = this.GetUserId();
        try
        {
            double monthlyReport = await _reportingService.GetMonthlyTotalsAsync(userId, year, month);
            return Ok(monthlyReport);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }

    [HttpGet("report/yearly")]
    [SwaggerOperation(description: "Get yearly report")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns double", typeof(double))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Yearly report not found")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authenticated")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
    public async Task<IActionResult> GetYearlyReport(int year)
    {
        int userId = this.GetUserId();
        try
        {
            double yearlyReport = await _reportingService.GetYearlyTotalsAsync(userId, year);
            return Ok(yearlyReport);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }
}