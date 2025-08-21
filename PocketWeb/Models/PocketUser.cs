namespace PocketWeb.Models;

public class PocketUser
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    // one-to-one
    //public PocketSettings? Settings { get; set; }

    // one-to-one
    //public PocketFamily? Family { get; set; }

    // many-to-many
    public List<PocketExpense> Expenses { get; set; } = [];

    // many-to-many
    public List<PocketRole> Roles { get; set; } = [];
}