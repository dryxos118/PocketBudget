using PocketApi.Models.Dto.User;

namespace PocketApi.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Get user info
    /// </summary>
    /// <param name="userId"></param>
    /// <returns><see cref="UserSummaryDto"/></returns>
    Task<UserSummaryDto> GetUserSummaryAsync(int userId);

    /// <summary>
    /// Get user setting
    /// </summary>
    /// <param name="userId"></param>
    /// <returns><see cref="UserSettingsDto"/></returns>
    Task<UserSettingsDto> GetUserSettingAsync(int userId);

    /// <summary>
    /// Update user info
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="userId"></param>
    /// <returns><see cref="bool"/></returns>
    Task<bool> UpdateUserSummaryAsync(int userId, UserSummaryDto dto);

    /// <summary>
    /// Update user setting
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="userId"></param>
    /// <returns><<see cref="bool"/>/returns>
    Task<bool> UpdateUserSettingAsync(int userId, UserSettingsDto dto);
}