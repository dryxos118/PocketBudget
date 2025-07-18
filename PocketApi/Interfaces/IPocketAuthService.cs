using PocketApi.Models.Dto;

namespace PocketApi.Interfaces
{
    public interface IPocketAuthService
    {
        Task<AuthResponseDto> Register(RegisterDto registerDto);

        Task<AuthResponseDto> Login(LoginDto loginDto);

        Task<string> RefreshToken(int userId);
    }
}
