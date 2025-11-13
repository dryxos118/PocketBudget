namespace PocketApi.Models.Dto.Expense;

public class YearSummaryDto
{
    public int Year { get; set; } = 0;
    public double Total { get; set; } = 0;
    public double TotalIncome { get; set; } = 0;
    public double TotalExpense { get; set; } = 0;
    public List<MonthExpenseSummaryDto> MonthExpenseSummary { get; set; } = [];
}

public class MonthExpenseSummaryDto
{
    public int Month { get; set; } = 0;
    public double Total { get; set; } = 0;
    public double TotalIncome { get; set; } = 0;
    public double TotalExpense { get; set; } = 0;
}