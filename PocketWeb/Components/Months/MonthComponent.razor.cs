using Microsoft.AspNetCore.Components;
using MudBlazor;
using PocketWeb.Models.Utils;
using PocketWeb.Services;

namespace PocketWeb.Components.Months;

public partial class MonthComponent : PocketComponentsBase
{
    [Parameter] public int Month { get; set; } = DateTime.Now.Month;

    [Inject] private IPocketApiService PocketApiService { get; set; } = null!;
    [Inject] private PocketDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private TabView CurrentTabView { get; set; } = TabView.Graph;

    protected override async Task OnInitializedAsync()
    {
        if (Month == 0)
        {
            Month = DateTime.Now.Month;
        }
        await Task.CompletedTask;
    }

    private async Task ChangeMonth(int month)
    {
        Month = month;
        await Task.CompletedTask;
    }
    
    private async Task ChangeTabView(TabView tabView)
    {
        CurrentTabView = tabView;
        await Task.CompletedTask;
    }
}