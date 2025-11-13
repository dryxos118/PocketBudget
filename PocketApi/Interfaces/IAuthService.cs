using PocketApi.Models.Dto;

namespace PocketApi.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Method for Login user
    /// </summary>
    /// <param name="dto"></param>
    /// <returns><see cref="AuthResponseDto"/></returns>
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    
    /// <summary>
    /// Method for Register user
    /// </summary>
    /// <param name="dto"></param>
    /// <returns><see cref="AuthResponseDto"/></returns>
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
}