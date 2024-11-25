using DnDCharCtor.Models;
using DnDCharCtor.Ui.Constants;
using Microsoft.AspNetCore.Components;

namespace DnDCharCtor.Ui.Pages;

public partial class EditCharacter
{
    [Parameter]
    public string Id { get; set; } = string.Empty;

    protected override async void OnInitialized()
    {
        if (string.IsNullOrWhiteSpace(Id) is false)
        {
            var guid = Guid.Parse(Id);
            await ViewModel.InitializeAsync(guid);
        }
        else
        {
            ViewModel.Initialize(Character.Empty);
        }

        base.OnInitialized();
    }

    private async Task SaveChanges()
    {
        var isValid = ViewModel.Validate();
        if (isValid is false) return;

        var isSaved = await ViewModel.SaveAsync();
        if (isSaved is false) return;

        NavigationManager.NavigateTo(Routes.CurrentCharacter);
    }
}