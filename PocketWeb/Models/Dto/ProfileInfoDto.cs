namespace PocketWeb.Models.Dto;

public class ProfileInfoDto
{
    public string FullName { get; set; } = string.Empty;
    public string Initial { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsDarkMode { get; set; } = true;
    public string Language { get; set; } = "fr-FR";
}