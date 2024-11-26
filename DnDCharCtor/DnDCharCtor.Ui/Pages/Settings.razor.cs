
using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace DnDCharCtor.Ui.Pages;

[Route(Routes.Settings)]
public partial class Settings : IDisposable
{

    [Inject]
    public SettingsViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeAsync();
        ViewModel.PropertyChanged += ViewModel_PropertyChanged;

        await base.OnInitializedAsync();
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);
        
        ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
    }



    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged).SafeFireAndForget(null);
    }
}