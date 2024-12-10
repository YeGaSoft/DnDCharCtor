using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace DnDCharCtor.Ui.Pages;

[Route(Routes.Home + Routes.Characters)]
public partial class Characters
{
    [Inject]
    public MainViewModel ViewModel { get; set; } = default!;
}