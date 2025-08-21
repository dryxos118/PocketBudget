using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
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
        Validator.TryValidateObject(model, validationContext, results,validateAllProperties: true);
        return results;
    }
}