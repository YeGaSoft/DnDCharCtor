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
        _eventSubscription = _eventAggregator.GetEvent<CurrentCharacterChangedEvent>().Subscribe(() => ReLoadCurrentCharacterAsync().SafeFireAndForget(null));
    }

    [ObservableProperty]
    private CharacterViewModel? _currentCharacterViewModel;

    [ObservableProperty]
    private bool _isBusy;

    public async Task<bool> InitializeAsync()
    {
        IsBusy = true;
        var selectedLanguage = await _hybridCacheService.GetSelectedLanguageAsync();
        _localizationService.ChangeCulture(selectedLanguage);

        await ReLoadCurrentCharacterAsync(true);
        
        IsBusy = false;
        return true;
    }

    public async Task<bool> ReLoadCurrentCharacterAsync(bool ignoreIsBusy = false)
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
