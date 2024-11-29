using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Common.Constants;
using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Resources;
using DnDCharCtor.Common.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly IHybridCacheService _hybridCacheService;

    public SettingsViewModel(IHybridCacheService hybridCacheService)
    {
        _hybridCacheService = hybridCacheService;
    }


    public Dictionary<string, CultureInfo> Languages { get; } = new Dictionary<string, CultureInfo>()
    {
        { StringResources.Settings_Languages_English, new CultureInfo(Culture.EnglishCultureIdentifier) },
        { StringResources.Settings_Languages_German, new CultureInfo(Culture.GermanCultureIdentifier) }
    };

    private KeyValuePair<string, CultureInfo> _selectedLanguage;
    public KeyValuePair<string, CultureInfo> SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            SetSelectedLanguageAsync(value).SafeFireAndForget(null);
        }
    }

    [ObservableProperty]
    private bool _isBusy;


    private async Task SetSelectedLanguageAsync(KeyValuePair<string, CultureInfo> value)
    {
        IsBusy = true;
        SetProperty(ref _selectedLanguage, value, nameof(SelectedLanguage));
        await _hybridCacheService.SetSelectedLanguageAsync(_selectedLanguage.Value);
        IsBusy = false;
    }



    public async Task<bool> InitializeAsync()
    {
        var selectedLanguage = await _hybridCacheService.GetSelectedLanguageAsync();
        SelectedLanguage = Languages.First(x => x.Value.Name == selectedLanguage.Name);
        return true;
    }
}
