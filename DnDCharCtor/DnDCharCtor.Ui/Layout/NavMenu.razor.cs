using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Common.Services;
using DnDCharCtor.Models;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DnDCharCtor.Ui.Layout;

public partial class NavMenu : IDisposable
{
    [Inject]
    public ILocalizationService LocalizationService { get; set; } = default!;

    [Inject]
    public EditCharacterViewModel EditCharacterViewModel { get; set; } = default!;

    [Inject]
    public MainViewModel ViewModel { get; set; } = default!;



    protected override void OnInitialized()
    {
        LocalizationService.PropertyChanged += PropertyChanged;
        ViewModel.PropertyChanged += PropertyChanged;
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        LocalizationService.PropertyChanged -= PropertyChanged;
        ViewModel.PropertyChanged -= PropertyChanged;
    }



    private void PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged).SafeFireAndForget(null);
    }
}