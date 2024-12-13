using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;

namespace DnDCharCtor.Ui.Pages;

[Route(Routes.Home + Routes.CurrentCharacter)]
public partial class CurrentCharacter
{
    [Inject]
    public MainViewModel ViewModel { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    private string? _searchText = string.Empty;
    private string? SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            InvokeAsync(StateHasChanged).SafeFireAndForget(null);
        }
    }

    private void EditCharacter()
    {
        var uri = NavigationManager.ToAbsoluteUri(Routes.EditCharacter);
        var query = new Dictionary<string, string?>
        {
            { Routes.EditCharacterQueryParameterForceNew, true.ToString() },
            { Routes.EditCharacterQueryParameterId, ViewModel.CurrentCharacterViewModel!.CharacterId.ToString() },
        };
        var newUri = QueryHelpers.AddQueryString(uri.ToString(), query);
        NavigationManager.NavigateTo(newUri);
    }
}