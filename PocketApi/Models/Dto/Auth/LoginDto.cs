using System.ComponentModel.DataAnnotations;

namespace PocketApi.Models.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email or Username is required.")]
        public string EmailOrUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}
