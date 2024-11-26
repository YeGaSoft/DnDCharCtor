using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Common.Resources;
using DnDCharCtor.Common.Services;
using DnDCharCtor.Models;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace DnDCharCtor.Ui.Pages;

[Route(Routes.EditCharacter)]
[Route(Routes.EditCharacterWithParameter)]
public partial class EditCharacter : IDisposable
{
    [Inject]
    public EditCharacterViewModel ViewModel { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public ILocalizationService LocalizationService { get; set; } = default!;

    [Parameter]
    public string Id { get; set; } = string.Empty;

    private string _title { get; set; } = string.Empty;

    private async Task SaveChanges()
    {
        var isValid = ViewModel.Validate();
        if (isValid is false) return;

        var isSaved = await ViewModel.SaveAsync();
        if (isSaved is false) return;

        NavigationManager.NavigateTo(Routes.CurrentCharacter);
    }



    protected override async Task OnInitializedAsync()
    {
        if (string.IsNullOrWhiteSpace(Id) is false)
        {
            var guid = Guid.Parse(Id);
            await ViewModel.InitializeAsync(guid);
            var characterName = ViewModel.CharacterViewModelToEdit.PersonalityViewModel.CharacterName;
            _title = string.Format(StringResources.CharacterEditor_Edit, characterName);
        }
        else
        {
            ViewModel.Initialize(Character.Empty);
            _title = StringResources.CharacterEditor_Create;
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
            _title = string.Format(StringResources.CharacterEditor_Edit, characterName);
        }
        else
        {
            _title = StringResources.CharacterEditor_Create;
        }

        InvokeAsync(StateHasChanged).SafeFireAndForget(null);
    }
}