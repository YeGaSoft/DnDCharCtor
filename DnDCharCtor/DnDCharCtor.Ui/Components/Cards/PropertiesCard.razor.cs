using DnDCharCtor.Resources;
using DnDCharCtor.Ui.Components.Dialogs;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components.Cards;

public partial class PropertiesCard
{
    [Inject]
    public Microsoft.FluentUI.AspNetCore.Components.IDialogService DialogService { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public PropertiesViewModel ViewModel { get; set; } = default!;

    [Parameter]
    public EventCallback<PropertiesViewModel> ViewModelChanged { get; set; }

    [Parameter]
    public string EditButtonText { get; set; } = StringResources.Button_Edit;

    [Parameter]
    public Icon EditButtonIcon { get; set; } = new Icons.Regular.Size20.Edit();

    private async Task EditProperties()
    {
        var data = new PropertiesViewModel(ViewModel);
        var dialogParameters = new Microsoft.FluentUI.AspNetCore.Components.DialogParameters()
        {
            Title = StringResources.CharacterEditor_Properties_Edit,
            //Width = "500px",
            PreventDismissOnOverlayClick = true,
            ShowDismiss = true,
        };
        var dialog = await DialogService.ShowDialogAsync<EditPropertiesDialog>(data, dialogParameters);
        var result = await dialog.Result;
        if (result.Cancelled is false && result.Data is not null)
        {
            ViewModel = (PropertiesViewModel)result.Data;
            // When we started Editing with Validation-Errors (because the user tried to Save before):
            // we want to re-validate after submit to set `HasValidationErrors` to false when all errors were resolved.
            if (ViewModel.HasValidationErrors)
            {
                ViewModel.Validate();
            }
            await ViewModelChanged.InvokeAsync(ViewModel);
            StateHasChanged();
        }
    }
}