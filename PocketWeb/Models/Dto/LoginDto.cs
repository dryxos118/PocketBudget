using System.ComponentModel.DataAnnotations;

namespace PocketWeb.Models.Dto;

public class LoginDto
{
    [Required(ErrorMessage = "{0} est requis")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} est requis")]
    public string Password { get; set; } = string.Empty;
}