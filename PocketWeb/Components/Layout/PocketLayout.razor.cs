using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using PocketWeb.Models.Dto;
using PocketWeb.Services;

namespace PocketWeb.Components.Layout;

public partial class PocketLayout : LayoutComponentBase
{
    [Inject] private IStringLocalizer<PocketLayout> Localizer { get; set; } = null!;
    [Inject] private IPocketApiService PocketApiService { get; set; } = null!;
    [Inject] private AuthentificationService AuthentificationService { get; set; } = null!;
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    
    private bool _drawerOpen = false;
    private PocketLayoutDto _layoutDto = new();

    protected override async Task OnInitializedAsync()
    {
        await GetLayoutInfo();
    }

    private async Task GetLayoutInfo()
    {
        PocketLayoutDto? layoutDto = await PocketApiService.GetAsync<PocketLayoutDto>("PocketLayout/GetLayout");
        if (layoutDto != null)
        {
            _layoutDto = layoutDto;
        }
    }
    
    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    private async Task Logout()
    {
        await AuthentificationService.LogoutAsync();
        (AuthenticationStateProvider as JwtTokenAuthenticationStateProvider)!.NotifyUserLogout();
        NavigationManager.NavigateTo("/login");
    }
}