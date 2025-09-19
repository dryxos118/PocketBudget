namespace PocketWeb.Models.Utils;

public class PocketDialogOption
{
    public bool DisplayOkBtn { get; set; } = true;
    public string OkText { get; set; } = string.Empty;
    public bool DisplayCancelBtn { get; set; } = true;
    public string CancelText { get; set; } = string.Empty;
    public PocketDialogSize DialogSize { get; set; } = PocketDialogSize.Medium;
}

public enum PocketDialogSize
{
    Small = 0,
    Medium = 1,
    Large = 2,
    Full = 3
}