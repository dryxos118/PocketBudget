namespace PocketApi.Models.Dto.User;

public class UserSummaryDto
{
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public PocketRole Role { get; set; }
    public bool Enabled { get; set; } = true;
}