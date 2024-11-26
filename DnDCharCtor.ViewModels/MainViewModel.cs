using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Common.Constants;
using DnDCharCtor.Common.Services;
using DnDCharCtor.Models;
using DnDCharCtor.ViewModels.ModelViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IHybridCacheService _hybridCacheService;
    private readonly ILocalizationService _localizationService;

    public MainViewModel(IHybridCacheService hybridCacheService, ILocalizationService localizationService)
    {
        _hybridCacheService = hybridCacheService;
        _localizationService = localizationService;
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

        var currentCharacter = await _hybridCacheService.GetCurrentCharacterAsync();
        CurrentCharacterViewModel = new(currentCharacter ?? Character.Empty);
        IsBusy = false;
        return true;
    }
}
