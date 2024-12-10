using DnDCharCtor.Common.Constants;
using DnDCharCtor.Resources;
using DnDCharCtor.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Common.Services;

public class HybridCacheService : IHybridCacheService
{
    private readonly IPlatformService _platformService;
    private readonly ILocalizationService _localizationService;

    public HybridCacheService(IPlatformService platformService, ILocalizationService localizationService)
    {
        _platformService = platformService;
        _localizationService = localizationService;
    }

    private IReadOnlyList<Character> _characters = [];
    public async Task<IReadOnlyList<Character>> GetCharactersAsync()
    {
        if (_characters.Count is 0)
        {
            _characters = await _platformService.GetFromStorageAsync<IReadOnlyList<Character>?>(StorageKeys.Characters).ConfigureAwait(false) ?? [];
        }

        return _characters;
    }

    
    private Character? _currentCharacter;
    public async Task<Character?> GetCurrentCharacterAsync()
    {
        if (_currentCharacter is null)
        {
            var currentCharacterId = await _platformService.GetFromStorageAsync<string?>(StorageKeys.CurrentCharacterId).ConfigureAwait(false);
            if (currentCharacterId is null) return null;

            var guid = Guid.Parse(currentCharacterId);

            if (_characters.Count is 0) await GetCharactersAsync().ConfigureAwait(false);
            _currentCharacter = _characters.FirstOrDefault(c => c.Id == guid);
        }

        return _currentCharacter;
    }

    public async Task<bool> SetCurrentCharacterAsync(Character character)
    {
        var characters = _characters.Where(c => c.Id != character.Id).Append(character);
        var isCharactersUpdated = await _platformService.SetInStorageAsync(StorageKeys.Characters, characters).ConfigureAwait(false);
        if (isCharactersUpdated)
        {
            _characters = characters.ToList();
            var isSaved = await _platformService.SetInStorageAsync(StorageKeys.CurrentCharacterId, character.Id.ToString()).ConfigureAwait(false);
            _currentCharacter = character;

            return isSaved;
        }

        return false;
    }


    private CultureInfo? _selectedLanguage;
    public async Task<CultureInfo> GetSelectedLanguageAsync()
    {
        if (_selectedLanguage is null)
        {
            var languageIdentifier = await _platformService.GetFromStorageAsync<string?>(StorageKeys.SelectedLanguageIdentifier).ConfigureAwait(false);
            if (languageIdentifier is null)
            {
                var systemLanguageIdentifier = await _platformService.GetSystemLanguageIdentifierAsync().ConfigureAwait(false);
                if (_localizationService.IsLanguageSupported(systemLanguageIdentifier)) await SetSelectedLanguageAsync(new CultureInfo(systemLanguageIdentifier)).ConfigureAwait(false);
            }
            else
            {
                _selectedLanguage = new CultureInfo(languageIdentifier);
            } 
        }

        return _selectedLanguage ?? new CultureInfo(Culture.EnglishCultureIdentifier); // We do not cache to enforce trying to get a supported language later
    }

    public async Task<bool> SetSelectedLanguageAsync(CultureInfo cultureInfo)
    {
        var isLanguageSupported = _localizationService.IsLanguageSupported(cultureInfo.Name);
        if (isLanguageSupported is false) return false;

        var isUpdated = await _platformService.SetInStorageAsync(StorageKeys.SelectedLanguageIdentifier, cultureInfo.Name).ConfigureAwait(false);
        if (isUpdated is false) return false;

        _selectedLanguage = cultureInfo;

        _localizationService.ChangeCulture(cultureInfo);

        return true;
    }
}
