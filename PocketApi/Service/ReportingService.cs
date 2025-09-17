using Microsoft.EntityFrameworkCore;
using PocketApi.Data;
using PocketApi.Interfaces;
using PocketApi.Models;

namespace PocketApi.Service;

public class ReportingService(PocketBudgetContext context) : IReportingService
{
    private readonly PocketBudgetContext _context = context;

    public async Task<double> GetMonthlyTotalsAsync(int userId, int year, int month)
    {
        List<PocketExpense> expenses = await _context.Expenses
            .Where(e => e.ExpenseDate.Year == year && e.ExpenseDate.Month == month && e.UserId == userId)
            .ToListAsync() ?? [];

        if (expenses.Count == 0)
        {
            return 0;
        }

        double totalAmount = CalculateTotalAmount(expenses);

        return totalAmount >= 0 ? Math.Abs(totalAmount) : -Math.Abs(totalAmount);
    }

    public async Task<double> GetYearlyTotalsAsync(int userId, int year)
    {
        List<PocketExpense> expenses = await _context.Expenses
            .Where(e => e.ExpenseDate.Year == year && e.UserId == userId)
            .ToListAsync();

        if (expenses.Count == 0)
        {
            return 0;
        }

        double totalAmount = CalculateTotalAmount(expenses);

        return totalAmount >= 0 ? Math.Abs(totalAmount) : -Math.Abs(totalAmount);
    }

    private static double CalculateTotalAmount(List<PocketExpense> expenses)
    {
        double totalExpenses = expenses.Where(e => e.ExpenseType == ExpenseType.Expense).Sum(e => e.ExpenseAmount);
        double totalIncomes = expenses.Where(e => e.ExpenseType == ExpenseType.Income).Sum(e => e.ExpenseAmount);
        return Math.Round(totalIncomes - totalExpenses, 2);
    }
}