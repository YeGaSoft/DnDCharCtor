using DnDCharCtor.Common.Services;
using DnDCharCtor.Models;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace DnDCharCtor.Ui.Pages;

[Route(Routes.Home)]
public partial class Home
{
    [Inject]
    public IHybridCacheService HybridCacheService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [CascadingParameter(Name = CascadeValueNames.DataContext)]
    public INotifyPropertyChanged? DataContext { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (DataContext is MainViewModel viewModel)
        {
            var currentCharacterViewModel = viewModel.CurrentCharacterViewModel;
            if (string.IsNullOrWhiteSpace(currentCharacterViewModel?.PersonalityViewModel?.CharacterName))
            {
                NavigationManager.NavigateTo(Routes.EditCharacter);
                return;
            }
        }
        else
        {
            var currentCharacter = await HybridCacheService.GetCurrentCharacterAsync();
            if (string.IsNullOrWhiteSpace(currentCharacter?.Personality?.CharacterName))
            {
                NavigationManager.NavigateTo(Routes.EditCharacter);
                return;
            }
        }

       NavigationManager.NavigateTo(Routes.CurrentCharacter);
    }
}