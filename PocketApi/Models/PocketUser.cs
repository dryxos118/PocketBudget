using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocketApi.Models
{
    [Table(nameof(PocketUser))]
    public class PocketUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("enabled")] public bool Enabled { get; set; } = true;

        [Required] [Column("role")] public PocketRole Role { get; set; } = PocketRole.PocketUser;

        // one-to-one
        public PocketUserSettings Settings { get; set; } = new();

        // one-to-many
        public List<PocketExpense> Expenses { get; set; } = [];
    }
}