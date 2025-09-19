using MudBlazor;
using PocketWeb.Components.Common;
using PocketWeb.Models.Utils;

namespace PocketWeb.Services;

public class PocketDialogService(IDialogService dialogService)
{
    private readonly IDialogService _dialogService = dialogService;

    public async Task<IDialogReference?> DisplayModalAsync(string title, PocketDialogOption dialogOptions,
        Type dynamicType, Dictionary<string, object?> parameters)
    {
        DialogParameters dialogParameters = new()
        {
            { nameof(PocketDialog.DialogOptions), dialogOptions },
            { nameof(PocketDialog.DynamicType), dynamicType },
            { nameof(PocketDialog.TypeParameters), parameters }
        };

        IDialogReference? dialogInstance = await _dialogService.ShowAsync<PocketDialog>(title, dialogParameters);

        return await Task.FromResult(dialogInstance);
    }
}