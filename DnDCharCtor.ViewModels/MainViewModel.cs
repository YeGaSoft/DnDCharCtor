using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Common.Constants;
using DnDCharCtor.Common.Events;
using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Common.Services;
using DnDCharCtor.Models;
using DnDCharCtor.ViewModels.ModelViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels;

public partial class MainViewModel : ObservableObject, IDisposable
{
    private readonly IHybridCacheService _hybridCacheService;
    private readonly ILocalizationService _localizationService;
    private readonly IEventAggregator _eventAggregator;

    private readonly SubscriptionToken _eventSubscription;

    public MainViewModel(IHybridCacheService hybridCacheService, ILocalizationService localizationService, IEventAggregator eventAggregator)
    {
        _hybridCacheService = hybridCacheService;
        _localizationService = localizationService;
        _eventAggregator = eventAggregator;
        _eventSubscription = _eventAggregator.GetEvent<CurrentCharacterChangedEvent>().Subscribe(() => ReloadCurrentCharacterAsync().SafeFireAndForget(null));
    }

    [ObservableProperty]
    private CharacterViewModel? _currentCharacterViewModel;

    [ObservableProperty]
    private bool _isBusy;

    private TaskCompletionSource<bool> _initializationTcs = new();
    public Task<bool> InitializationTask => _initializationTcs.Task;
    public async Task<bool> InitializeAsync()
    {
        _initializationTcs = new();

        IsBusy = true;
        var selectedLanguage = await _hybridCacheService.GetSelectedLanguageAsync();
        _localizationService.ChangeCulture(selectedLanguage);

        await ReloadCurrentCharacterAsync(true);
        
        IsBusy = false;

        _initializationTcs.SetResult(true);
        return await _initializationTcs.Task;
    }

    public async Task<bool> ReloadCurrentCharacterAsync(bool ignoreIsBusy = false)
    {
        if (ignoreIsBusy) IsBusy = true;
        var currentCharacter = await _hybridCacheService.GetCurrentCharacterAsync();
        CurrentCharacterViewModel = new(currentCharacter ?? Character.Empty);
        if (ignoreIsBusy) IsBusy = false;

        return true;
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _eventSubscription?.Dispose();
    }
}
