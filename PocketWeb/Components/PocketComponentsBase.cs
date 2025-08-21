using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using PocketWeb.Localization;

namespace PocketWeb.Components;

public class PocketComponentsBase : ComponentBase
{
    [Inject] public IStringLocalizer<PocketTrad> S { get; set; } = default!;

    protected bool IsInitialized = false;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            IsInitialized = true;
        }
        await base.OnAfterRenderAsync(firstRender);
    }
}