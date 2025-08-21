namespace PocketWeb.Config;

public static class WebApplicationExtensions
{
    public static void AddAppLocalization(this WebApplication app, ConfigurationManager configuration)
    {
        var cultures = configuration.GetSection("Cultures")
            .GetChildren().ToDictionary(x => x.Key, x => x.Value);

        var supportedCultures = cultures.Keys.ToArray();

        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture("fr-FR")
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);
    }
}