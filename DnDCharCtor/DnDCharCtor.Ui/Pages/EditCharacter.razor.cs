using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Resources;
using DnDCharCtor.Common.Services;
using DnDCharCtor.Models;
using DnDCharCtor.Ui.Components.Dialogs;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDCharCtor.Ui.Pages;

[Route(Routes.EditCharacter)]
public partial class EditCharacter : IDisposable
{
    [Inject]
    public EditCharacterViewModel ViewModel { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public ILocalizationService LocalizationService { get; set; } = default!;

    [Inject]
    public Microsoft.FluentUI.AspNetCore.Components.IDialogService DialogService { get; set; } = default!;


    [Parameter]
    [SupplyParameterFromQuery(Name = Routes.EditCharacterQueryParameterForceNew)]
    public bool ForceNew { get; set; } = false;

    [Parameter]
    [SupplyParameterFromQuery(Name = Routes.EditCharacterQueryParameterId)]
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    private async Task SaveChanges()
    {
        var isValid = ViewModel.Validate();
        if (isValid is false)
        {
            var dialogParameters = new Microsoft.FluentUI.AspNetCore.Components.DialogParameters()
            {
                Title = StringResources.Validation_CannotSaveTitle,
                PreventDismissOnOverlayClick = true,
                ShowDismiss = true,
            };
            await DialogService.ShowDialogAsync<ValidationErrorDialog>(ViewModel, dialogParameters);
            return;
        }

        var isSaved = await ViewModel.SaveAsync();
        if (isSaved is false) return;

        NavigationManager.NavigateTo(Routes.CurrentCharacter);
    }


    // ToDo: When switching pages the old EditCharachterViewModel should be loaded
    protected override async Task OnInitializedAsync()
    {
        if (ForceNew)
        {
            if (string.IsNullOrWhiteSpace(Id) is false)
            {
                var guid = Guid.Parse(Id);
                await ViewModel.InitializeAsync(guid);
                var characterName = ViewModel.CharacterViewModelToEdit.PersonalityViewModel.CharacterName;
                Title = string.Format(StringResources.CharacterEditor_Edit, characterName);
            }
            else
            {
                ViewModel.Initialize(Character.Empty);
                Title = StringResources.CharacterEditor_Create;
            }
        }

        LocalizationService.PropertyChanged += LocalizationService_PropertyChanged;

        await base.OnInitializedAsync();
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        LocalizationService.PropertyChanged -= LocalizationService_PropertyChanged;
    }



    private void LocalizationService_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Id) is false)
        {
            var characterName = ViewModel.CharacterViewModelToEdit.PersonalityViewModel.CharacterName;
            Title = string.Format(StringResources.CharacterEditor_Edit, characterName);
        }
        else
        {
            Title = StringResources.CharacterEditor_Create;
        }

        InvokeAsync(StateHasChanged).SafeFireAndForget(null);
    }
}