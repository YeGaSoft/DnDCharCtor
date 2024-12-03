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

    [Parameter]
    [EditorRequired]
    public CharacterViewModel? ViewModel { get; set; }



    protected override void OnInitialized()
    {
        LocalizationService.PropertyChanged += LocalizationService_PropertyChanged;
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        LocalizationService.PropertyChanged -= LocalizationService_PropertyChanged;
    }



    private void LocalizationService_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged).SafeFireAndForget(null);
    }
}