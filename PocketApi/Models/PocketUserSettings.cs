using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocketApi.Models;

[Table(nameof(PocketUserSettings))]
public class PocketUserSettings
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("user_setting_id")]
    public int UserSettingId { get; set; }

    [Column("avatar_url")] public string AvatarUrl { get; set; } = string.Empty;

    [Column("theme")] public Theme Theme { get; set; } = Theme.System;

    [Column("currency")] [MaxLength(3)] public string Currency { get; set; } = "EUR";

    [Column("language")] [MaxLength(10)] public string Language { get; set; } = "fr-FR";
    
    [Column("date_format")] [MaxLength(20)] public string DateFormat { get; set; } = "DD/MM/YYYY";

    [Column("user_id")] public int UserId { get; set; }

    // one-to-one
    public PocketUser? User { get; set; }
}

public enum Theme
{
    [Description("Light")]
    Light,
    [Description("Dark")]
    Dark,
    [Description("System")]
    System
}