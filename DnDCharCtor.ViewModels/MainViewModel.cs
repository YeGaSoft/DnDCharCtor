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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels;

public partial class MainViewModel : ObservableObject, IDisposable
{
    private readonly IHybridCacheService _hybridCacheService;
    private readonly ILocalizationService _localizationService;
    private readonly IEventAggregator _eventAggregator;

    private readonly SubscriptionToken _currentCharacterEventSubscription;
    private readonly SubscriptionToken _charactersEventSubscription;

    public MainViewModel(IHybridCacheService hybridCacheService, ILocalizationService localizationService, IEventAggregator eventAggregator)
    {
        _hybridCacheService = hybridCacheService;
        _localizationService = localizationService;
        _eventAggregator = eventAggregator;
        _currentCharacterEventSubscription = _eventAggregator.GetEvent<CurrentCharacterChangedEvent>().Subscribe(() => ReloadCurrentCharacterAsync().SafeFireAndForget(null));
        _charactersEventSubscription = _eventAggregator.GetEvent<CharactersChangedEvent>().Subscribe(() => ReloadCharactersAsync().SafeFireAndForget(null));
    }

    [ObservableProperty]
    private CharacterViewModel? _currentCharacterViewModel;

    [ObservableProperty]
    private IReadOnlyList<CharacterViewModel> _characters = [];

    [ObservableProperty]
    private bool _isBusy;

    private readonly SemaphoreSlim _initializationSemaphore = new(1);
    private TaskCompletionSource<bool> _initializationTcs = new();
    public Task<bool> InitializationTask => _initializationTcs.Task;
    public async Task<bool> InitializeAsync()
    {
        try
        {
            await _initializationSemaphore.WaitAsync();
            _initializationTcs = new();

            IsBusy = true;
            var selectedLanguage = await _hybridCacheService.GetSelectedLanguageAsync();
            _localizationService.ChangeCulture(selectedLanguage);

            await ReloadCharactersAsync(true);
            await ReloadCurrentCharacterAsync(true);

            _initializationTcs.SetResult(true);
            return await _initializationTcs.Task;
        }
        catch
        {
            _initializationTcs.SetResult(false);
            return await _initializationTcs.Task;
        }
        finally
        {
            IsBusy = false;

            _initializationSemaphore.Release();
        }
    }

    public async Task<bool> ReloadCurrentCharacterAsync(bool ignoreIsBusy = false)
    {
        if (ignoreIsBusy) IsBusy = true;
        var currentCharacter = await _hybridCacheService.GetCurrentCharacterAsync();
        CurrentCharacterViewModel = new(currentCharacter ?? Character.Empty);

        // ReloadCurrentCharacterAsync is also called when the current character was removed or a new one was added - thus, the number of characters changed.
        await ReloadCharactersAsync(ignoreIsBusy);

        if (ignoreIsBusy) IsBusy = false;

        return true;
    }

    public async Task<bool> ReloadCharactersAsync(bool ignoreIsBusy = false)
    {
        if (ignoreIsBusy) IsBusy = true;
        var characters = await _hybridCacheService.GetCharactersAsync();
        Characters = characters.Select(character => new CharacterViewModel(character)).ToList();
        if (ignoreIsBusy) IsBusy = false;

        return true;
    }



    ~MainViewModel() => Dispose();

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _currentCharacterEventSubscription?.Dispose();
        _charactersEventSubscription?.Dispose();
    }
}
