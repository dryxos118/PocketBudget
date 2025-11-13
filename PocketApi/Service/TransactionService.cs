using System.Text;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PocketApi.Data;
using PocketApi.Interfaces;
using PocketApi.Mappers;
using PocketApi.Models;
using PocketApi.Models.Dto.Expense;
using PocketApi.Models.Utils;

namespace PocketApi.Service;

public class TransactionService(PocketBudgetContext context) : ITransactionService
{
    private readonly PocketBudgetContext _context = context;

    public async Task<MonthSummaryDto> GetMonthSummaryAsync(int userId, int year, int month)
    {
        List<PocketExpense> expenses = await _context.Expenses
            .Where(e => e.ExpenseDate.Year == year && e.ExpenseDate.Month == month && e.UserId == userId)
            .ToListAsync();

        return expenses.Count == 0 ? new MonthSummaryDto() : GenerateMonthSumary(expenses, month);
    }

    public async Task<YearSummaryDto> GetYearSummaryAsync(int userId, int year)
    {
        List<PocketExpense> expenses = await _context.Expenses
            .Where(e => e.ExpenseDate.Year == year && e.UserId == userId)
            .ToListAsync();

        return expenses.Count == 0 ? new YearSummaryDto() : GenerateYearSumary(expenses, year);
    }

    public async Task<bool> AddTransactionAsync(int userId, ExpenseDto? expense)
    {
        if (expense == null)
            throw new PocketActionResult("Expenses cannot be null", ErrorType.BadRequest);

        PocketExpense newExpense = expense.ToEntity(userId);
        await _context.Expenses.AddAsync(newExpense);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateTransactionAsync(int userId, ExpenseDto? expense)
    {
        if (expense == null)
            throw new PocketActionResult("Expenses cannot be null", ErrorType.BadRequest);

        PocketExpense? existingExpense = await _context.Expenses.FindAsync(expense.ExpenseId);
        if (existingExpense == null)
            throw new PocketActionResult("Expense does not exist", ErrorType.BadRequest);

        existingExpense.UpdateEntity(expense);
        _context.Expenses.Update(existingExpense);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteTransactionAsync(int userId, int expenseId)
    {
        PocketExpense? existingExpense = await _context.Expenses.FindAsync(expenseId);
        if (existingExpense == null)
            throw new PocketActionResult("Expense does not exist", ErrorType.BadRequest);

        _context.Expenses.Remove(existingExpense);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<byte[]> ExportTransactionAsync(int userId, int year, string format = "xlsx")
    {
        List<PocketExpense> expenses =
            await _context.Expenses.Where(e => e.UserId == userId && e.ExpenseDate.Year == year).ToListAsync();

        if (expenses.Count == 0)
            throw new PocketActionResult("No expenses found", ErrorType.NotFound);

        switch (format)
        {
            case "xlsx":
            {
                MemoryStream ms = new MemoryStream();
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet($"ExpensesForYear_{year}");

                ICellStyle headerStyle = workbook.CreateCellStyle();
                IFont headerFont = workbook.CreateFont();
                headerFont.IsBold = true;
                headerStyle.SetFont(headerFont);
                headerStyle.Alignment = HorizontalAlignment.Center;

                IRow headerRow = sheet.CreateRow(0);
                List<string> columns = ["Name", "Amount", "Date", "Type", "Category"];
                for (int i = 0; i < columns.Count; i++)
                {
                    CreateCell(headerRow, i, columns[i], headerStyle);
                }

                int rowIndex = 1;
                foreach (PocketExpense expense in expenses)
                {
                    IRow row = sheet.CreateRow(rowIndex++);
                    CreateCell(row, 0, expense.ExpenseName);
                    CreateCell(row, 1, expense.ExpenseAmount.ToString("C"));
                    CreateCell(row, 2, expense.ExpenseDate.ToString("dd/MM/yyyy HH:mm"));
                    CreateCell(row, 3, expense.ExpenseType.ToString());
                    CreateCell(row, 4, expense.ExpenseCategory.ToString());
                }

                for (int i = 0; i < columns.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }

                workbook.Write(ms, true);
                byte[] xlsxBytes = ms.ToArray();

                return xlsxBytes;
            }
            case "csv":
            {
                StringBuilder stringBuilder = new();
                stringBuilder.Append('\uFEFF');
                stringBuilder.AppendLine("Name,Amount,Date,Type,Category");

                foreach (PocketExpense expense in expenses)
                {
                    string name = EscapeString(expense.ExpenseName);
                    string amount = expense.ExpenseAmount.ToString("C").Replace(",", ".");
                    string date = expense.ExpenseDate.ToString("dd/MM/yyyy HH:mm");
                    string type = expense.ExpenseType.ToString();
                    string category = expense.ExpenseCategory.ToString();

                    stringBuilder.AppendLine($"{name},{amount},{date},{type},{category}");
                }

                byte[] csvBytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());

                return csvBytes;
            }
        }

        throw new PocketActionResult("Export format invalid", ErrorType.BadRequest);
    }

    private static string EscapeString(string s)
    {
        if (string.IsNullOrEmpty(s))
            return "";

        string escaped = s.Replace("\n", "\"\"");
        if (escaped.Contains(',') || escaped.Contains('"') || escaped.Contains('\n'))
            return $"\"{escaped}\"";
        return escaped;
    }

    private static void CreateCell(IRow row, int cellIndex, string value, ICellStyle? style = null)
    {
        ICell cell = row.CreateCell(cellIndex);
        cell.SetCellValue(value);

        if (style != null)
            cell.CellStyle = style;
    }

    private Totals ComputeTotals(List<PocketExpense> expenses)
    {
        double totalExpense = expenses.Where(e => e.ExpenseType == ExpenseType.Expense).Sum(s => s.ExpenseAmount);
        double totalIncome = expenses.Where(e => e.ExpenseType == ExpenseType.Income).Sum(s => s.ExpenseAmount);
        double totalAmount = Math.Round(totalIncome - totalExpense, 2);

        return new Totals
        {
            TotalExpenses = totalExpense,
            TotalIncomes = totalIncome,
            TotalAmount = totalAmount
        };
    }

    private MonthSummaryDto GenerateMonthSumary(List<PocketExpense> expenses, int month)
    {
        Totals totals = ComputeTotals(expenses);

        IEnumerable<CategorySumaryDto> categorySumary = expenses.GroupBy(x => x.ExpenseCategory).Select(x =>
            new CategorySumaryDto
            {
                Category = x.Key,
                TotalExpense = x.Where(e => e.ExpenseType == ExpenseType.Expense).Sum(s => s.ExpenseAmount),
                TotalIncome = x.Where(e => e.ExpenseType == ExpenseType.Income).Sum(s => s.ExpenseAmount)
            });

        return new MonthSummaryDto
        {
            Month = month,
            Expenses = expenses.ToDtoList(),
            Total = totals.TotalAmount,
            TotalIncome = totals.TotalIncomes,
            TotalExpense = totals.TotalExpenses,
            CategorySumary = categorySumary.ToList()
        };
    }

    private YearSummaryDto GenerateYearSumary(List<PocketExpense> expenses, int year)
    {
        Totals totals = ComputeTotals(expenses);

        List<MonthExpenseSummaryDto> monthExpenseSummary = Enumerable.Range(1, 12).Select(m =>
        {
            List<PocketExpense> expensesThisMonth = expenses.Where(e => e.ExpenseDate.Month == m).ToList();

            Totals computeTotals = ComputeTotals(expensesThisMonth);

            return new MonthExpenseSummaryDto
            {
                Month = m,
                Total = computeTotals.TotalAmount,
                TotalIncome = computeTotals.TotalIncomes,
                TotalExpense = computeTotals.TotalExpenses
            };
        }).ToList();

        return new YearSummaryDto
        {
            Year = year,
            Total = totals.TotalAmount,
            TotalIncome = totals.TotalIncomes,
            TotalExpense = totals.TotalExpenses,
            MonthExpenseSummary = monthExpenseSummary.ToList()
        };
    }
}