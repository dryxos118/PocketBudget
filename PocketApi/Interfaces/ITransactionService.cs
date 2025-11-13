using PocketApi.Models;
using PocketApi.Models.Dto.Expense;

namespace PocketApi.Interfaces;

public interface ITransactionService
{
    /// <summary>
    /// Get summary for month
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <returns><see cref="MonthSummaryDto"/></returns>
    Task<MonthSummaryDto> GetMonthSummaryAsync(int userId, int year, int month);
    
    /// <summary>
    /// Get summary for year
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="year"></param>
    /// <returns><see cref="YearSummaryDto"/></returns>
    Task<YearSummaryDto> GetYearSummaryAsync(int userId, int year);
    
    /// <summary>
    /// Add transaction
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="expense"></param>
    /// <returns><see cref="bool"/></returns>
    Task<bool> AddTransactionAsync(int userId, ExpenseDto? expense);

    /// <summary>
    /// Update transaction
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="expense"></param>
    /// <returns><see cref="bool"/></returns>
    Task<bool> UpdateTransactionAsync(int userId, ExpenseDto? expense);

    /// <summary>
    /// Delete transaction
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="expenseId"></param>
    /// <returns><see cref="bool"/></returns>
    Task<bool> DeleteTransactionAsync(int userId, int expenseId);

    /// <summary>
    /// Export all transaction by year
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="year"></param>
    /// <param name="format"></param>
    /// <returns><see cref="byte[]"/></returns>
    Task<byte[]> ExportTransactionAsync(int userId, int year, string format = "xlsx");
}