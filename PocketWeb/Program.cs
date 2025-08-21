using MudBlazor;
using MudBlazor.Services;
using PocketWeb.Components;
using PocketWeb.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices(opt =>
{
    opt.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
    opt.SnackbarConfiguration.PreventDuplicates = false;
    opt.SnackbarConfiguration.NewestOnTop = false;
    opt.SnackbarConfiguration.ShowCloseIcon = true;
    opt.SnackbarConfiguration.HideTransitionDuration = 500;
    opt.SnackbarConfiguration.ShowTransitionDuration = 500;
    opt.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
builder.Services.AddLocalization();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddPocketBudgetService();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthorization();

app.UseAuthentication();

app.AddAppLocalization(builder.Configuration);

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseStatusCodePagesWithReExecute("/");
app.UseAntiforgery();

app.Run();