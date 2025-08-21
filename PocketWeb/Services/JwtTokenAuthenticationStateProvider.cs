using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace PocketWeb.Services;

public class JwtTokenAuthenticationStateProvider(AuthentificationService authentificationService)
    : AuthenticationStateProvider
{
    private readonly AuthentificationService _authentificationService = authentificationService;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await _authentificationService.GetAuthenticateAsync();
        return new AuthenticationState(principal);
    }

    public void NotifyUserAuthentication()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}