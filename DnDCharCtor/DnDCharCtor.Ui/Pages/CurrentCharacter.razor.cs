using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace DnDCharCtor.Ui.Pages;

[Route(Routes.Home + Routes.CurrentCharacter)]
public partial class CurrentCharacter
{
    [Inject]
    MainViewModel ViewModel { get; set; } = default!;
}