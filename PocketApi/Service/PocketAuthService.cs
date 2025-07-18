using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PocketApi.Data;
using PocketApi.Interfaces;
using PocketApi.Models;
using PocketApi.Models.Config;
using PocketApi.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace PocketApi.Service
{
    public class PocketAuthService(IOptions<TokenSettings> tokenOptions, PocketBudgetContext budgetContext) : IPocketAuthService
    {
        public IOptions<TokenSettings> TokenSettings { get; set; } = tokenOptions;

        private readonly PocketBudgetContext _budgetContext = budgetContext;

        public async Task<AuthResponseDto> Register(RegisterDto registerDto)
        {
            if (!IsValidEmail(registerDto.Email))
            {
                throw new PocketActionResult("Invalid email format.", ErrorType.BadRequest);
            }

            if (!IsPasswordStrong(registerDto.Password))
            {
                throw new PocketActionResult("Password does not meet the required strength criteria.", ErrorType.BadRequest);
            }

            if (_budgetContext.Users.Any(x => x.Email.Equals(registerDto.Email, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new PocketActionResult($"{registerDto.Email} is already in use.", ErrorType.BadRequest);
            }

            string encodedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            PocketUser newUser = new()
            {
                Email = registerDto.Email,
                Password = encodedPassword,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
            };

            await _budgetContext.Users.AddAsync(newUser);
            await _budgetContext.SaveChangesAsync();

            return new()
            {
                FullName = $"{newUser.FirstName} {newUser.LastName}",
                Token = CreateJwtToken(newUser)
            };
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            PocketUser? user = await _budgetContext.Users
                .FirstOrDefaultAsync(x => x.Email.Equals(loginDto.Email, StringComparison.InvariantCultureIgnoreCase));

            if (user == null)
            {
                throw new PocketActionResult($"User not found with this email: {loginDto.Email}", ErrorType.NotFound, "AuthService.UserLogin");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                throw new PocketActionResult("Password does not match.", ErrorType.BadRequest, "AuthService.UserLogin");
            }

            return new()
            {
                FullName = $"{user.FirstName} {user.LastName}",
                Token = CreateJwtToken(user)
            };
        }

        public Task<string> RefreshToken(int userId)
        {
            throw new NotImplementedException();
        }

        private string CreateJwtToken(PocketUser user)
        {
            if (user != null)
            {
                SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(TokenSettings.Value.Key));
                SigningCredentials credentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                List<Claim> claims =
                [
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("EmailIdentifier",user.Email),
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                ];

                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                }

                JwtSecurityToken jwtToken = new(
                    issuer: TokenSettings.Value.Issuer,
                    audience: TokenSettings.Value.Audience,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials,
                    claims: claims
                );

                string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                return token;
            }
            else
            {
                throw new PocketActionResult("User is null", ErrorType.InternalServerError, "AuthService.CreateJwtToken");
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email)) return false;

            try
            {
                MailAddress mailAddress = new(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsPasswordStrong(string password)
        {
            if (password.Length < 8)
                return false;

            if (!password.Any(char.IsUpper))
                return false;

            if (!password.Any(char.IsLower))
                return false;

            if (!password.Any(char.IsDigit))
                return false;

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                return false;

            return true;
        }
    }
}
