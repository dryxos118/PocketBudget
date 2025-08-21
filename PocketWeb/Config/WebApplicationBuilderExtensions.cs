using PocketWeb.Services;

namespace PocketWeb.Config;

public static class WebApplicationBuilderExtensions
{
    public static void AddPocketBudgetService(this IServiceCollection services)
    {
        services.AddScoped<IPocketApiService, PocketApiService>();
    }
}