using DnDCharCtor.Resources;
using DnDCharCtor.Ui.Components.Dialogs;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace DnDCharCtor.Ui.Components.Cards;

public partial class CharacterCard
{
    [Inject]
    public Microsoft.FluentUI.AspNetCore.Components.IDialogService DialogService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public MainViewModel MainViewModel { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public CharacterViewModel ViewModel { get; set; } = default!;

    [Parameter]
    public EventCallback<CharacterViewModel> ViewModelChanged { get; set; }

    private void EditCharacter()
    {
        var uri = NavigationManager.ToAbsoluteUri(Routes.EditCharacter);
        var query = new Dictionary<string, string?>
        {
            { Routes.EditCharacterQueryParameterForceNew, true.ToString() },
            { Routes.EditCharacterQueryParameterId, ViewModel.CharacterId.ToString() },
        };
        var newUri = QueryHelpers.AddQueryString(uri.ToString(), query);
        NavigationManager.NavigateTo(newUri);
    }

    private void SelectCharacter()
    {
        MainViewModel.CurrentCharacterViewModel = ViewModel;
        NavigationManager.NavigateTo(Routes.CurrentCharacter);
    }

    private async Task DeleteCharacterAsync()
    {
        var dialogParameters = new Microsoft.FluentUI.AspNetCore.Components.DialogParameters()
        {
            Title = StringResources.Dialog_DeleteCharacterTitle,
            PreventDismissOnOverlayClick = false,
            ShowDismiss = true,
        };
        var dialog = await DialogService.ShowDialogAsync<DeleteCharacterDialog>(ViewModel, dialogParameters);
        var result = await dialog.Result;
        if (result.Cancelled)
        {
            return;
        }
    }
}