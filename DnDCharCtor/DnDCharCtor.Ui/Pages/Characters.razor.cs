using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace DnDCharCtor.Ui.Pages;

[Route(Routes.Home + Routes.Characters)]
public partial class Characters : IDisposable
{
    [Inject]
    public MainViewModel ViewModel { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;



    protected override void OnInitialized()
    {
        ViewModel.PropertyChanged += PropertyChanged;
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        ViewModel.PropertyChanged -= PropertyChanged;
    }



    private void PropertyChanged(object? sender, EventArgs e)
    {
        PropertyChangedAsync().SafeFireAndForget(null);
    }

    private async Task PropertyChangedAsync()
    {
        await InvokeAsync(StateHasChanged);

        // When all characters were deleted we want to navigate to the Character-Editor
        // (not Character-Creator since it is possible that the user started creating a new character).
        if (ViewModel.Characters.Count is 0) NavigationManager.NavigateTo(Routes.EditCharacter);
    }
}