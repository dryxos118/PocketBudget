using PocketApi.Interfaces;
using PocketApi.Service;

namespace PocketApi.Config
{
    public static class WebApplicationBuilderExtension
    {
        public static void AddPocketBudgetService(this IServiceCollection services)
        {
            services.AddScoped<IPocketAuthService, PocketAuthService>();
        }
    }
}
