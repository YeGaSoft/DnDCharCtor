using DnDCharCtor.Models;
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
        // Trigger validation and save logic here
        // Validate all characters and show errors if any
        await Task.Delay(1);
    }
}