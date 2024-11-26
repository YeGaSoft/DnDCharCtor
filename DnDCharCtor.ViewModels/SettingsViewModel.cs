using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Common.Constants;
using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Common.Resources;
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
            IsBusy = true;
            SetProperty(ref _selectedLanguage, value);
            AsyncHelper.RunSync(() => _hybridCacheService.SetSelectedLanguageAsync(_selectedLanguage.Value));
            IsBusy = false;
        }
    }

    [ObservableProperty]
    private bool _isBusy;



    public async Task<bool> InitializeAsync()
    {
        var selectedLanguage = await _hybridCacheService.GetSelectedLanguageAsync();
        SelectedLanguage = Languages.First(x => x.Value.Name == selectedLanguage.Name);
        return true;
    }
}
