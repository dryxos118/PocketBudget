using EnumsNET;
using Microsoft.EntityFrameworkCore;
using PocketApi.Data;
using PocketApi.Interfaces;
using PocketApi.Models;
using PocketApi.Models.Dto.User;

namespace PocketApi.Service;

public class UserService(PocketBudgetContext context) : IUserService
{
    private readonly PocketBudgetContext _context = context;

    public async Task<UserSummaryDto> GetUserSummaryAsync(int userId)
    {
        PocketUser? user = await _context.Users.FindAsync(userId);

        if (user == null)
            throw new PocketActionResult("User not found", ErrorType.NotFound);

        return new UserSummaryDto
        {
            Id = userId,
            Email = user.Email,
            Username = user.Username,
            Role = user.Role,
            Enabled = user.Enabled,
        };
    }

    public async Task<UserSettingsDto> GetUserSettingAsync(int userId)
    {
        PocketUser? user = await _context.Users.Include(x => x.Settings).FirstAsync(x => x.UserId == userId);

        if (user == null)
            throw new PocketActionResult("User not found", ErrorType.NotFound);

        PocketUserSettings settings = user.Settings;

        return new UserSettingsDto
        {
            AvatarUrl = settings.AvatarUrl,
            Theme = settings.Theme,
            Language = settings.Language,
            DateFormat = settings.DateFormat,
            Currency = settings.Currency,
        };
    }

    // TODO
    public async Task<bool> UpdateUserSummaryAsync(int userId, UserSummaryDto dto)
    {
        throw new NotImplementedException();
    }

    // TODO
    public async Task<bool> UpdateUserSettingAsync(int userId, UserSettingsDto dto)
    {
        throw new NotImplementedException();
    }
}