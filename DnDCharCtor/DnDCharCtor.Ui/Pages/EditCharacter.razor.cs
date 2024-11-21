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
            ViewModel.Initialize(new());
        }

        base.OnInitialized();
    }
}