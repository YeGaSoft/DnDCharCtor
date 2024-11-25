using DnDCharCtor.Common.Resources;
using DnDCharCtor.Models;
using DnDCharCtor.Ui.Constants;
using Microsoft.AspNetCore.Components;

namespace DnDCharCtor.Ui.Pages;

public partial class EditCharacter
{
    [Parameter]
    public string Id { get; set; } = string.Empty;

    private string _title { get; set; }

    private async Task SaveChanges()
    {
        var isValid = ViewModel.Validate();
        if (isValid is false) return;

        var isSaved = await ViewModel.SaveAsync();
        if (isSaved is false) return;

        NavigationManager.NavigateTo(Routes.CurrentCharacter);
    }



    protected override async void OnInitialized()
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

        base.OnInitialized();
    }
}