using DnDCharCtor.Common.Events;
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

    [Inject]
    public IEventAggregator EventAggregator { get; set; } = default!;

    private SubscriptionToken? _eventSubscription;



    protected override void OnInitialized()
    {
        LocalizationService.PropertyChanged += PropertyChanged;
        ViewModel.PropertyChanged += PropertyChanged;
        _eventSubscription = EventAggregator.GetEvent<EditModeChangedEvent>().Subscribe(() => PropertyChanged(EventAggregator, EventArgs.Empty));

    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        LocalizationService.PropertyChanged -= PropertyChanged;
        ViewModel.PropertyChanged -= PropertyChanged;
        _eventSubscription?.Dispose();
    }



    private void PropertyChanged(object? sender, EventArgs e)
    {
        InvokeAsync(StateHasChanged).SafeFireAndForget(null);
    }
}