namespace PocketApi.Interfaces;

public interface IReportingService
{
    /// <summary>
    /// Get monthly totals by years and month
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <returns><see cref="double"/></returns>
    Task<double> GetMonthlyTotalsAsync(int userId, int year, int month);
    
    /// <summary>
    /// Get yearly totals by year
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="year"></param>
    /// <returns><see cref="double"/></returns>
    Task<double> GetYearlyTotalsAsync(int userId, int year);
}