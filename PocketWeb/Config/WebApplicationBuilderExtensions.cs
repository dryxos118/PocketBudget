using Microsoft.AspNetCore.Components.Authorization;
using PocketWeb.Services;

namespace PocketWeb.Config;

public static class WebApplicationBuilderExtensions
{
    public static void AddPocketBudgetService(this IServiceCollection services)
    {
        services.AddScoped<AuthentificationService>();
        services.AddScoped<AuthenticationStateProvider, JwtTokenAuthenticationStateProvider>();
        services.AddScoped<IPocketApiService, PocketApiService>();
    }
}