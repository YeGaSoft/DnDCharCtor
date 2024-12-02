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

    protected override void OnInitialized()
    {
        var currentCharacterViewModel = ViewModel.CurrentCharacterViewModel;
        if (string.IsNullOrWhiteSpace(currentCharacterViewModel?.PersonalityViewModel.CharacterName))
        {
            var uri = NavigationManager.ToAbsoluteUri(Routes.EditCharacter);
            var query = new Dictionary<string, string?>
            {
                { Routes.EditCharacterQueryParameterForceNew, true.ToString() },
            };
            var newUri = QueryHelpers.AddQueryString(uri.ToString(), query);
            NavigationManager.NavigateTo(newUri);
            //NavigationManager.NavigateTo(Routes.EditCharacter);
            return;
        }

        NavigationManager.NavigateTo(Routes.CurrentCharacter);
    }
}