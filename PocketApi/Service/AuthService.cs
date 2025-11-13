using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PocketApi.Data;
using PocketApi.Interfaces;
using PocketApi.Models;
using PocketApi.Models.Config;
using PocketApi.Models.Dto;

namespace PocketApi.Service;

public class AuthService(IOptions<TokenSettings> options, PocketBudgetContext context) : IAuthService
{
    private readonly IOptions<TokenSettings> _options = options;

    private readonly PocketBudgetContext _context = context;

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        PocketUser? user = await _context.Users.FirstOrDefaultAsync(x =>
            x.Email == dto.EmailOrUsername || x.Username == dto.EmailOrUsername);

        if (user == null)
            throw new PocketActionResult("User not found", ErrorType.NotFound);

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            throw new PocketActionResult("Password does not match", ErrorType.BadRequest);

        return new AuthResponseDto
        {
            Email = user.Email,
            Username = user.Username,
            Token = CreateJwtToken(user)
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        if (_context.Users.Any(u => u.Email == dto.Email))
            throw new PocketActionResult("Email already exists", ErrorType.BadRequest);

        if (_context.Users.Any(u => u.Username == dto.Username))
            throw new PocketActionResult("Username already exists", ErrorType.BadRequest);

        if (!IsValidEmail(dto.Email))
            throw new PocketActionResult("Invalid email format", ErrorType.BadRequest);

        if (!IsValidPassword(dto.Password))
            throw new PocketActionResult("Password does not meet the required strength criteria.",
                ErrorType.BadRequest);

        string encodedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        bool isFirstUser = !_context.Users.Any();

        PocketUser newUser = new()
        {
            Email = dto.Email,
            Username = dto.Username,
            Password = encodedPassword,
            Enabled = true,
            Role = isFirstUser ? PocketRole.PocketAdmin : PocketRole.PocketUser,
            Settings = new PocketUserSettings()
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            Email = newUser.Email,
            Username = newUser.Username,
            Token = CreateJwtToken(newUser),
        };
    }

    private string CreateJwtToken(PocketUser user)
    {
        if (user == null)
            throw new PocketActionResult("User not found", ErrorType.NotFound);

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_options.Value.Key));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        ];

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _options.Value.Issuer,
            audience: _options.Value.Audience,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials,
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            MailAddress mail = new MailAddress(email);
            return mail.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private bool IsValidPassword(string password)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            return false;

        if (!password.Any(char.IsUpper))
            return false;

        if (!password.Any(char.IsLower))
            return false;

        if (!password.Any(char.IsDigit))
            return false;

        if (password.All(char.IsLetterOrDigit))
            return false;

        return password.Length >= 8;
    }
}