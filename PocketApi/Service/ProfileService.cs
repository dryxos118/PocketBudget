using PocketApi.Data;
using PocketApi.Interfaces;
using PocketApi.Models;
using PocketApi.Models.Dto;

namespace PocketApi.Service;

public class ProfileService(PocketBudgetContext context) : IProfileService
{
    private readonly PocketBudgetContext _context = context;

    public async Task<ProfileInfoDto> GetProfileInfoAsync(int userId)
    {
        PocketUser? user = await context.Users.FindAsync(userId);
        if (user == null)
            throw new PocketActionResult("User not found.", ErrorType.BadRequest, "PocketLayoutService.GetLayoutAsync");

        return new ProfileInfoDto()
        {
            FullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Initial = string.Concat(
                user.FirstName.Substring(0, 1).ToUpperInvariant(),
                user.LastName.Substring(0, 1).ToUpperInvariant()),
        };
    }
}