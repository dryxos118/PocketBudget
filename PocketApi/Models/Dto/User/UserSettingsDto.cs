namespace PocketApi.Models.Dto.User;

public class UserSettingsDto
{
    public string AvatarUrl { get; set; }
    public Theme Theme { get; set; }
    public string Currency { get; set; }
    public string Language { get; set; }
    public string DateFormat { get; set; }
}