namespace PocketApi.Models.Dto.Expense;

public class MonthSummaryDto
{
    public int Month { get; set; } = 0;
    public List<ExpenseDto> Expenses { get; set; } = [];
    public double Total { get; set; } = 0;
    public double TotalExpense { get; set; } = 0;
    public double TotalIncome { get; set; } = 0;
    public List<CategorySumaryDto> CategorySumary { get; set; } = [];
}

public class CategorySumaryDto
{
    public CategoryExpense Category { get; set; }
    public double TotalExpense { get; set; } = 0;
    public double TotalIncome { get; set; } = 0;
}