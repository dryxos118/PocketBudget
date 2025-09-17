namespace PocketWeb.Models.Utils;

public interface IPocketDialogComponent
{
    public Task<PocketDialogResult> GetDialogResult();
}

public class PocketDialogResult
{
    public bool Valid { get; set; } = true;

    public object? Data { get; set; }
}