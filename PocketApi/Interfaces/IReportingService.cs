namespace PocketApi.Interfaces;

public interface IReportingService
{
    Task<double> GetMonthlyTotalsAsync(int userId, int year, int month);
    Task<double> GetYearlyTotalsAsync(int userId, int year);
}