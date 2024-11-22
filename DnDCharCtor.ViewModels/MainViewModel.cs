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

    public MainViewModel(IHybridCacheService hybridCacheService)
    {
        _hybridCacheService = hybridCacheService;
    }

    [ObservableProperty]
    private CharacterViewModel? _currentCharacterViewModel;

    public async Task<bool> InitializeAsync()
    {
        var currentCharacter = await _hybridCacheService.GetCurrentCharacterAsync();
        CurrentCharacterViewModel = new(currentCharacter ?? Character.Empty);
        return true;
    }
}
