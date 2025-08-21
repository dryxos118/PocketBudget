namespace PocketWeb.Models;

public class PocketRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = string.Empty;

    // many-to-many
    public List<PocketUser> Users { get; set; } = [];
}