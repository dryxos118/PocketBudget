using System.ComponentModel.DataAnnotations;

namespace PocketApi.Models.Dto.Expense;

public class ExpenseDto
{
    public int? ExpenseId { get; set; }

    [Required(ErrorMessage = "Expense type is required")]
    public ExpenseType ExpenseType { get; set; } = ExpenseType.Expense;

    [Required(ErrorMessage = "Expense name is required")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "Name must be at least 3 characters long")]
    public string ExpenseName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Expense date is required")]
    public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Expense amount is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Expense amount must be > 0")]
    public double ExpenseAmount { get; set; } = 0;

    [Required(ErrorMessage = "Expense category is required")]
    public CategoryExpense ExpenseCategory { get; set; } = CategoryExpense.Miscellaneous;
}