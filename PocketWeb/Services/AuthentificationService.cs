using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace PocketWeb.Services;

public class AuthentificationService(
    ILogger<AuthentificationService> logger,
    ProtectedSessionStorage protectedSessionStorage,
    HttpClient httpClient)
{
    private readonly ILogger<AuthentificationService> _logger = logger;
    private readonly ProtectedSessionStorage _protectedSessionStorage = protectedSessionStorage;
    private readonly HttpClient _httpClient = httpClient;

    public async Task<ClaimsPrincipal> GetAuthenticateAsync()
    {
        string? token = await GetTokenAsync();
        ClaimsIdentity identity = new();
        _httpClient.DefaultRequestHeaders.Authorization = null;
        if (!string.IsNullOrEmpty(token))
        {
            identity = new ClaimsIdentity(await GetClaimsAsync(token.Replace("\"", "")),
                JwtBearerDefaults.AuthenticationScheme);
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
        }

        ClaimsPrincipal principal = new(identity);
        return principal;
    }

    public async Task LogoutAsync()
    {
        await SetTokenAsync(null);
    }

    public async Task<string?> GetTokenAsync()
    {
        var sessionToken = await _protectedSessionStorage.GetAsync<string>("token");

        return sessionToken.Success ? sessionToken.Value : null;
    }

    public async Task SetTokenAsync(string? token)
    {
        if (string.IsNullOrEmpty(token))
        {
            await _protectedSessionStorage.DeleteAsync("token");
        }
        else
        {
            await _protectedSessionStorage.SetAsync("token", token);
        }
    }

    public async Task<List<Claim>> GetClaimsAsync(string? token)
    {
        List<Claim> claims = [];
        if (!string.IsNullOrEmpty(token))
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            claims = jwtSecurityToken.Claims.ToList();
        }

        return await Task.FromResult(claims);
    }
}