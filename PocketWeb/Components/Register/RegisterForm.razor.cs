using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PocketWeb.Models.Dto;
using PocketWeb.Services;

namespace PocketWeb.Components.Register;

public partial class RegisterForm : PocketComponentsBase
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IPocketApiService PocketApiService { get; set; } = null!;
    [Inject] private AuthentificationService AuthentificationService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    private RegisterDto _register = new();
    private bool _isSubmitting;

    private InputType _passwordInputType = InputType.Password;
    private string _passwordIcon = Icons.Material.Filled.VisibilityOff;
    private InputType _confirmInputType = InputType.Password;
    private string _confirmIcon = Icons.Material.Filled.VisibilityOff;

    private void TogglePassword()
    {
        if (_passwordInputType == InputType.Password)
        {
            _passwordInputType = InputType.Text;
            _passwordIcon = Icons.Material.Filled.Visibility;
        }
        else
        {
            _passwordInputType = InputType.Password;
            _passwordIcon = Icons.Material.Filled.VisibilityOff;
        }
    }

    private void ToggleConfirm()
    {
        if (_confirmInputType == InputType.Password)
        {
            _confirmInputType = InputType.Text;
            _confirmIcon = Icons.Material.Filled.Visibility;
        }
        else
        {
            _confirmInputType = InputType.Password;
            _confirmIcon = Icons.Material.Filled.VisibilityOff;
        }
    }

    private async Task HandleValidSubmit()
    {
        _isSubmitting = true;

        AuthResponseDto? authResponseDto =
            await PocketApiService.PostAsync<AuthResponseDto, RegisterDto>("Auth/Register", _register);

        if (authResponseDto != null)
        {
            await AuthentificationService.SetTokenAsync(authResponseDto.Token);
            
            (AuthenticationStateProvider as JwtTokenAuthenticationStateProvider)!.NotifyUserAuthentication();
            
            Snackbar.Add(
                new MarkupString($"<div>Inscription réussie !<div/><div>Bonjour {authResponseDto.FullName} !!</div>"),
                Severity.Success);

            await Task.Delay(1000);

            NavigationManager.NavigateTo("/");
        }
        else
        {
            Snackbar.Add("Il y a une erreur dans l'inscription", Severity.Error);
        }
        
        _isSubmitting = false;
    }
}