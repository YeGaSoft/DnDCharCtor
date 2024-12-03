using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Resources;
using DnDCharCtor.Models;
using DnDCharCtor.Ui.Components.Dialogs;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DnDCharCtor.Ui.Components.Cards;

public partial class PersonalityCard
{
    [Inject]
    public Microsoft.FluentUI.AspNetCore.Components.IDialogService DialogService { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public PersonalityViewModel ViewModel { get; set; } = default!;

    [Parameter]
    public EventCallback<PersonalityViewModel> ViewModelChanged { get; set; }

    private async Task EditPersonality()
    {
        var data = new PersonalityViewModel(ViewModel);
        var dialogParameters = new Microsoft.FluentUI.AspNetCore.Components.DialogParameters()
        {
            Title = StringResources.CharacterEditor_Personality_Edit,
            //Width = "500px",
            PreventDismissOnOverlayClick = true,
            ShowDismiss = true,
        };
        var dialog = await DialogService.ShowDialogAsync<EditPersonalityDialog>(data, dialogParameters);
        var result = await dialog.Result;
        if (result.Cancelled is false && result.Data is not null)
        {
            ViewModel = (PersonalityViewModel)result.Data;
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