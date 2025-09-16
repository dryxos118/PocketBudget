using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PocketApi.Data;
using PocketApi.Models;

namespace PocketApi.Test;

public class TestUtils
{
    public static PocketBudgetContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<PocketBudgetContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new PocketBudgetContext(options);
    }

    public static PocketExpense GetPocketExpense(int id, DateTime date)
    {
        PocketExpense pocketExpense = new()
        {
            UserId = 1,
            ExpenseAmount = 100,
            ExpenseCategory = CategoryExpense.Miscellaneous,
            ExpenseDate = date,
            ExpenseId = id,
            ExpenseName = "Test",
            ExpenseType = ExpenseType.Expense
        };
        return pocketExpense;
    }

    public static List<ValidationResult> ValidateModel(object model)
    {
        var results = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);
        Validator.TryValidateObject(model, validationContext, results, validateAllProperties: true);
        return results;
    }

    public static void AssertPocketController(IActionResult result, ErrorType errorType)
    {
        ObjectResult obj = Assert.IsType<ObjectResult>(result, exactMatch: false);
        Assert.Equal((int)errorType, obj.StatusCode);

        string json = JsonConvert.SerializeObject(obj.Value);
        using var doc = JsonDocument.Parse(json);

        Assert.Equal(errorType.ToString(), doc.RootElement.GetProperty("type").GetString());
    }

    public static void SetupControllerContext(ControllerBase controller, string? controllerName = null,
        string? actionName = null,
        HttpContext? httpContext = null)
    {
        controller.ControllerContext = new()
        {
            ActionDescriptor = new()
            {
                ControllerName = controllerName ?? "TestController",
                ActionName = actionName ?? "TestAction",
            },
            HttpContext = httpContext ?? new DefaultHttpContext()
        };
    }
}