
using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Common.Services;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDCharCtor.Ui.Pages;

[Route(Routes.Home + Routes.Settings)]
public partial class Settings : IDisposable
{
    [Inject]
    public SettingsViewModel ViewModel { get; set; } = default!;

    [Inject]
    public ILocalizationService LocalizationService { get; set; } = default!;

    public DesignThemeModes Mode { get; set; }
    public OfficeColor? OfficeColor { get; set; }



    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeAsync();
        ViewModel.PropertyChanged += PropertyChanged;
        LocalizationService.PropertyChanged += PropertyChanged; 

        await InvokeAsync(StateHasChanged);
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);
        
        ViewModel.PropertyChanged -= PropertyChanged;
        LocalizationService.PropertyChanged -= PropertyChanged;
    }



    private void PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged).SafeFireAndForget(null);
    }
}