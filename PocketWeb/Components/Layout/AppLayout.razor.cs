using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PocketWeb.Components.Common;
using PocketWeb.Models.Dto;
using PocketWeb.Models.Utils;
using PocketWeb.Services;

namespace PocketWeb.Components.Layout;

public partial class AppLayout : LayoutComponentBase
{
    [Inject] private IPocketApiService PocketApiService { get; set; } = null!;
    [Inject] private AuthentificationService AuthentificationService { get; set; } = null!;
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private PocketDialogService DialogService { get; set; } = null!;

    private bool _drawerOpen;
    private ProfileInfoDto _currentProfile = new();

    private readonly NavItem[] _navItems =
    [
        new NavItem("/", "Store", Icons.Material.Filled.Store),
        new NavItem("/month", "Library", Icons.Material.Filled.CalendarMonth),
        new NavItem("/years", "Community", Icons.Material.Filled.CalendarViewMonth, Disabled: true)
    ];

    private void ToggleDrawer() => _drawerOpen = !_drawerOpen;

    private Task OnDrawerChanged(bool open)
    {
        _drawerOpen = open;
        return Task.CompletedTask;
    }

    protected override async Task OnInitializedAsync()
    {
        await GetCurrentProfile();
    }

    private async Task GetCurrentProfile()
    {
        ProfileInfoDto? infoDto = await PocketApiService.GetAsync<ProfileInfoDto>("portal/profile");
        if (infoDto != null)
        {
            _currentProfile = infoDto;
        }
    }

    private async Task DisplayConfirmLogoutDialog()
    {
        PocketDialogOption option = new PocketDialogOption()
        {
            DialogSize = PocketDialogSize.Medium,
            OkText = "Oui",
            CancelText = "Non"
        };

        Dictionary<string, object?> parameters = new()
        {
            { nameof(ConfirmDialog.Message), "Etes vous sur de vouloir vous déconecter ?" }
        };

        IDialogReference? dialog =
            await DialogService.DisplayModalAsync("Se déconnecter", option, typeof(ConfirmDialog), parameters);

        if (dialog != null)
        {
            DialogResult? res = await dialog.Result;

            if (res != null && !res.Canceled)
            {
                await Logout();
            }
        }
    }

    private async Task Logout()
    {
        await AuthentificationService.LogoutAsync();
        (AuthenticationStateProvider as JwtTokenAuthenticationStateProvider)!.NotifyUserLogout();
        NavigationManager.NavigateTo("/login");
    }
}