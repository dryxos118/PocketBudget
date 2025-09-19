using Microsoft.AspNetCore.Components;
using MudBlazor;
using PocketWeb.Models.Utils;

namespace PocketWeb.Components.Common;

public partial class PocketDialog : ComponentBase
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    [Parameter] public PocketDialogOption DialogOptions { get; set; } = new();

    [Parameter] public Type? DynamicType { get; set; }

    [Parameter] public Dictionary<string, object?> TypeParameters { get; set; } = new();

    private DynamicComponent? DynamicComponentRef { get; set; }

    private string GetDialogSize()
    {
        return DialogOptions.DialogSize switch
        {
            PocketDialogSize.Small => "width:25%;",
            PocketDialogSize.Medium => "width:50%;",
            PocketDialogSize.Large => "width:75%;",
            PocketDialogSize.Full => "width:100%;",
            _ => "width:50%;"
        };
    }

    public void Cancel() => MudDialog.Cancel();

    public async Task Submit()
    {
        if (typeof(IPocketDialogComponent).IsAssignableFrom(DynamicType))
        {
            IPocketDialogComponent? instance = (IPocketDialogComponent?)DynamicComponentRef?.Instance;
            PocketDialogResult res = await instance!.GetDialogResult();

            if (res.Valid)
            {
                MudDialog.Close(MudBlazor.DialogResult.Ok(res.Data, res.Data?.GetType()));
            }
        }
    }
}