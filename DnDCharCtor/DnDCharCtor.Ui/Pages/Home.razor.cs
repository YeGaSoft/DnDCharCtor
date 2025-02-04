using DnDCharCtor.Common.Services;
using DnDCharCtor.Models;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel;

namespace DnDCharCtor.Ui.Pages;

[Route(Routes.Home)]
public partial class Home
{
    [Inject]
    public IHybridCacheService HybridCacheService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public MainViewModel ViewModel { get; set; } = default!;



    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializationTask;
        await InvokeAsync(StateHasChanged);
        var currentCharacterViewModel = ViewModel.CurrentCharacterViewModel;
        if (string.IsNullOrWhiteSpace(currentCharacterViewModel?.PersonalityViewModel.CharacterName))
        {
            NavigationManager.NavigateTo(Routes.CreateCharacter);
            return;
        }

        NavigationManager.NavigateTo(Routes.CurrentCharacter);
    }
}