using Microsoft.AspNetCore.Mvc;
using PocketApi.Config;
using PocketApi.Interfaces;
using PocketApi.Models;
using PocketApi.Models.Dto.Expense;
using Swashbuckle.AspNetCore.Annotations;

namespace PocketApi.Controllers;

[Produces("application/json")]
[Route("api/v1/transactions")]
[ApiController]
public class TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService)
    : Controller
{
    private readonly ILogger<TransactionController> _logger = logger;
    private readonly ITransactionService _transactionService = transactionService;

    [HttpGet("monthly")]
    [SwaggerOperation(Summary = "Get Month Summary", Description = "Get Month Summary")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(MonthSummaryDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> GetMonthSummary(int year, int month)
    {
        try
        {
            int userId = this.GetUserId();

            MonthSummaryDto monthSummary = await _transactionService.GetMonthSummaryAsync(userId, year, month);

            return Ok(monthSummary);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }

    [HttpGet("yearly")]
    [SwaggerOperation(Summary = "Get Year Summary", Description = "Get Year Summary")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(YearSummaryDto))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> GetYearSummary(int year)
    {
        try
        {
            int userId = this.GetUserId();

            YearSummaryDto yearSummary = await _transactionService.GetYearSummaryAsync(userId, year);

            return Ok(yearSummary);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }

    [HttpPost("add")]
    [SwaggerOperation(Summary = "Add Transaction", Description = "Add Transaction")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(bool))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> AddTransaction([FromBody] ExpenseDto expense)
    {
        try
        {
            this.HandleModelValidation(_logger);

            int userId = this.GetUserId();

            bool success = await _transactionService.AddTransactionAsync(userId, expense);

            return Ok(success);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }

    [HttpPut("update")]
    [SwaggerOperation(Summary = "Update Transaction", Description = "Update Transaction")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(bool))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> UpdateTransaction([FromBody] ExpenseDto expense)
    {
        try
        {
            this.HandleModelValidation(_logger);

            int userId = this.GetUserId();

            bool success = await _transactionService.UpdateTransactionAsync(userId, expense);

            return Ok(success);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }

    [HttpDelete("delete")]
    [SwaggerOperation(Summary = "Delete Transaction", Description = "Delete Transaction")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(bool))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> DeleteTransaction(int expenseId)
    {
        try
        {
            int userId = this.GetUserId();

            bool success = await _transactionService.DeleteTransactionAsync(userId, expenseId);

            return Ok(success);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }

    [HttpGet("export")]
    [SwaggerOperation(Summary = "Export Transactions", Description = "Export Transactions")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok Success", typeof(FileContentResult))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
    public async Task<IActionResult> ExportTransaction(int year, string format = "xlsx")
    {
        try
        {
            int userId = this.GetUserId();

            string fileName = $"ExpenseForYear_{year}";
            string mimeType = format switch
            {
                "xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "csv" => "text/csv; charset=utf-8",
                _ => throw new PocketActionResult("Format not supported", ErrorType.BadRequest)
            };

            byte[] fileContent = await _transactionService.ExportTransactionAsync(userId, year, format);

            return File(fileContent, mimeType, fileName);
        }
        catch (PocketActionResult ex)
        {
            return this.HandlePocketActionResult(ex, _logger);
        }
    }
}