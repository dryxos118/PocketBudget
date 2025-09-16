using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PocketWeb.Models.Dto;
using PocketWeb.Services;

namespace PocketWeb.Components.Login;

public partial class LoginForm : PocketComponentsBase
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IPocketApiService PocketApiService { get; set; } = null!;
    [Inject] private AuthentificationService AuthentificationService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    private bool _isSubmitting;
    private LoginDto _login = new();
    private InputType _inputType = InputType.Password;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

    private void ShowHidePassword()
    {
        if (_inputType == InputType.Password)
        {
            _passwordInputIcon = Icons.Material.Filled.Visibility;
            _inputType = InputType.Text;
        }
        else
        {
            _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
            _inputType = InputType.Password;
        }
    }

    private async Task HandleValidSubmit()
    {
        _isSubmitting = true;

        if (!string.IsNullOrEmpty(_login.Email) && !string.IsNullOrEmpty(_login.Password))
        {
            AuthResponseDto? authResponseDto =
                await PocketApiService.PostAsync<AuthResponseDto, LoginDto>("Auth/Login", _login);

            if (authResponseDto != null)
            {
                await AuthentificationService.SetTokenAsync(authResponseDto.Token);

                (AuthenticationStateProvider as JwtTokenAuthenticationStateProvider)!.NotifyUserAuthentication();
                
                Snackbar.Add(
                    new MarkupString($"<div>Connexion réussie !<div/><div>Bonjour {authResponseDto.FullName} !!</div>"),
                    Severity.Success);

                await Task.Delay(1000);

                NavigationManager.NavigateTo("/");
            }
            else
            {
                Snackbar.Add("Identifiants incorrects. Veuillez réessayer.", Severity.Error);
            }
        }
        else
        {
            Snackbar.Add("Veuillez remplir tous les champs.", Severity.Error);
        }

        _isSubmitting = false;
    }
}