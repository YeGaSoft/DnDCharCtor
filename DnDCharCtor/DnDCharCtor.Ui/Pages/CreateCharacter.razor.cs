using DnDCharCtor.Ui.Constants;
using Microsoft.AspNetCore.Components;

namespace DnDCharCtor.Ui.Pages;

[Route(Routes.Home + Routes.CreateCharacter)]
public partial class CreateCharacter
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    protected override void OnInitialized()
    {
        NavigationManager.NavigateTo(Routes.EditCharacterWithForceNew);
    }
}