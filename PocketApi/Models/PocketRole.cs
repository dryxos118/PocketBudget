using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocketApi.Models
{
    [Table(nameof(PocketRole))]
    public class PocketRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("role_id")]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("role_name")]
        public string RoleName { get; set; } = string.Empty;

        [Column("role_description")]
        public string RoleDescription { get; set; } = string.Empty;

        public List<PocketUser> Users { get; set; } = [];
    }
}
