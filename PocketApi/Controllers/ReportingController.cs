using Microsoft.AspNetCore.Mvc;
using PocketApi.Config;
using PocketApi.Interfaces;
using PocketApi.Models;
using PocketApi.Models.Dto.User;
using Swashbuckle.AspNetCore.Annotations;

namespace PocketApi.Controllers;

[Produces("application/json")]
[Route("api/v1/reporting")]
[ApiController]
public class ReportingController(ILogger<ReportingController> logger, IReportingService reportingService) : Controller
{
    private readonly ILogger<ReportingController> _logger = logger;
    private readonly IReportingService _reportingService = reportingService;

    [HttpGet("monthly")]
    [SwaggerOperation(Summary = "Get Monthly Totals", Description = "Get Monthly Totals")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(double))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> GetMonthly(int year, int month)
    {
        try
        {
            int userId = this.GetUserId();

            double amount = await _reportingService.GetMonthlyTotalsAsync(userId, year, month);

            return Ok(amount);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }

    [HttpGet("yearly")]
    [SwaggerOperation(Summary = "Get Yearly Totals", Description = "Get Yearly Totals")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(double))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> GetYearly(int year)
    {
        try
        {
            int userId = this.GetUserId();

            double amount = await _reportingService.GetYearlyTotalsAsync(userId, year);

            return Ok(amount);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }
}