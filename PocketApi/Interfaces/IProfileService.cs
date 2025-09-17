using PocketApi.Models.Dto;

namespace PocketApi.Interfaces;

public interface IProfileService
{
    Task<ProfileInfoDto> GetProfileInfoAsync(int userId);
}