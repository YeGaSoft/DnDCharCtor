using DnDCharCtor.Resources;
using DnDCharCtor.Ui.Components.Dialogs;
using DnDCharCtor.ViewModels;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Ui.Components.Cards;

/// <summary>
/// When using this Base-Class, the razor-file must use '@inherits GenericCard&lt;TViewModel, TDialog&gt;' -
/// otherwise there is an error that the partial classes inherit from different base classes.
/// </summary>
public abstract partial class GenericCard<TViewModel, TDialog> : ComponentBase
    where TViewModel : IViewModelBase<TViewModel>
    where TDialog : IDialogContentComponent<TViewModel>
{
    [Inject]
    public Microsoft.FluentUI.AspNetCore.Components.IDialogService DialogService { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public TViewModel ViewModel { get; set; } = default!;

    [Parameter]
    public EventCallback<TViewModel> ViewModelChanged { get; set; }

    [Parameter]
    public string EditButtonText { get; set; } = StringResources.Button_Edit;

    [Parameter]
    public Icon EditButtonIcon { get; set; } = new Icons.Regular.Size20.Edit();

    public async Task EditAsync()
    {
        var data = ViewModel.CreateShallowCopy();
        var dialogParameters = new Microsoft.FluentUI.AspNetCore.Components.DialogParameters()
        {
            Title = StringResources.CharacterEditor_Edit,
            //Width = "500px",
            PreventDismissOnOverlayClick = true,
            ShowDismiss = true,
        };
        var dialog = await DialogService.ShowDialogAsync<TDialog>(data, dialogParameters);
        var result = await dialog.Result;
        if (result.Cancelled is false && result.Data is not null)
        {
            ViewModel = (TViewModel)result.Data;
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
