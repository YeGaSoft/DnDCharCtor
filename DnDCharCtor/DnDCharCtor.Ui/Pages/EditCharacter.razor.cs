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
using DnDCharCtor.Ui.Components.Cards;

namespace DnDCharCtor.Ui.Pages;

[Route(Routes.Home + Routes.EditCharacter)]
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

    private IEditableCard personalityCardRef = default!;
    private IEditableCard propertiesCardRef = default!;
    private IEditableCard rescueDiceCardRef = default!;
    private IEditableCard skillsCardRef = default!;


    private string? _searchText = string.Empty;
    private string? SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            InvokeAsync(StateHasChanged).SafeFireAndForget(null);
        }
    }


    private void Reset()
    {
        ViewModel.Reset();
    }

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



    protected override async Task OnInitializedAsync()
    {
        LocalizationService.PropertyChanged += LocalizationService_PropertyChanged;

        if (ForceNew && ViewModel.HasUnsavedChanges())
        {
            var dialogParameters = new Microsoft.FluentUI.AspNetCore.Components.DialogParameters()
            {
                Title = StringResources.Dialog_DiscardTitle,
                PreventDismissOnOverlayClick = true,
                ShowDismiss = false,
            };
            var dialog = await DialogService.ShowDialogAsync<UnsavedChangedDialog>(dialogParameters);
            var result = await dialog.Result;
            if (result.Cancelled)
            {
                return;
            }
        }

        if (ForceNew || ViewModel.IsSaved || ViewModel.IsDefault())
        {
            if (string.IsNullOrWhiteSpace(Id) is false)
            {
                var guid = Guid.Parse(Id);
                await ViewModel.InitializeAsync(guid);
            }
            else
            {
                ViewModel.Initialize(Character.Empty, EditMode.Create);
            }
        }

        await InvokeAsync(StateHasChanged);
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        LocalizationService.PropertyChanged -= LocalizationService_PropertyChanged;
    }



    private void LocalizationService_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged).SafeFireAndForget(null);
    }
}