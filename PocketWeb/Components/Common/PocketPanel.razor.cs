using Microsoft.AspNetCore.Components;

namespace PocketWeb.Components.Common;

public partial class PocketPanel : PocketComponentsBase
{
    [Parameter, EditorRequired] public RenderFragment? PanelContent { get; set; }
    [Parameter] public PanelVariant Variant { get; set; } = PanelVariant.Primary;
    [Parameter] public string? Title { get; set; }
    [Parameter] public string? Icon { get; set; }
    [Parameter] public bool ShowTitle { get; set; } = true;
    [Parameter] public bool TitleCentered { get; set; } = false;
    [Parameter] public string? Class { get; set; }
    [Parameter] public string? ChildClass { get; set; }

    private string? PanelClass => Variant switch
    {
        PanelVariant.Primary => "pocket-panel",
        PanelVariant.Secondary => "pocket-panel secondary",
        _ => null,
    };  

    private bool IsPrimary => Variant == PanelVariant.Primary;

    private bool ShowHeader => !string.IsNullOrEmpty(Title) && ShowTitle;

    private string? HeaderClass => TitleCentered ? "ms-2 mb-0 align-items-center" : "ms-2 mb-0";

    private string? ChildClassName => $"fs-5 {ChildClass}" ?? string.Empty;
}

public enum PanelVariant
{
    Primary,
    Secondary,
}