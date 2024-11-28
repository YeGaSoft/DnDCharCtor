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
    public IDialogService DialogService { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public PersonalityViewModel ViewModel { get; set; } = default!;

    [Parameter]
    public EventCallback<PersonalityViewModel> ViewModelChanged { get; set; }

    private async Task EditPersonality()
    {
        var data = new PersonalityViewModel(ViewModel);
        var dialogParameters = new DialogParameters()
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
            await ViewModelChanged.InvokeAsync(ViewModel);
            StateHasChanged();
        }
    }
}