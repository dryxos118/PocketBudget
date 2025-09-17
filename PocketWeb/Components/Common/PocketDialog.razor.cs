using Microsoft.AspNetCore.Components;
using MudBlazor;
using PocketWeb.Models.Utils;

namespace PocketWeb.Components.Common;

public partial class PocketDialog : ComponentBase
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    [Parameter] public DialogOptions DialogOptions { get; set; } = new();

    [Parameter] public Type? DynamicType { get; set; }

    [Parameter] public Dictionary<string, object?> TypeParameters { get; set; } = new();

    private DynamicComponent? DynamicComponentRef { get; set; }

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