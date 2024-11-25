using DnDCharCtor.Common.Constants;
using DnDCharCtor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Common.Services;

public class HybridCacheService : IHybridCacheService
{
    private readonly IPlatformService _platformService;

    public HybridCacheService(IPlatformService platformService)
    {
        _platformService = platformService;
    }

    private IReadOnlyList<Character> _characters = [];
    public async Task<IReadOnlyList<Character>> GetCharactersAsync()
    {
        if (_characters.Count == 0)
        {
            _characters = await _platformService.GetFromStorageAsync<IReadOnlyList<Character>?>(StorageKeys.Characters) ?? [];
        }

        return _characters;
    }

    
    private Character? _currentCharacter;
    public async Task<Character?> GetCurrentCharacterAsync()
    {
        if (_currentCharacter is null)
        {
            var currentCharacterId = await _platformService.GetFromStorageAsync<string?>(StorageKeys.CurrentCharacterId);
            if (currentCharacterId is null) return null;

            var guid = Guid.Parse(currentCharacterId);
            _currentCharacter = _characters.FirstOrDefault(c => c.Id == guid);
        }

        return _currentCharacter;
    }

    public async Task<bool> SetCurrentCharacterAsync(Character character)
    {
        var characters = _characters.Append(character);
        var isCharactersUpdated = await _platformService.SetInStorageAsync(StorageKeys.Characters, characters);
        if (isCharactersUpdated)
        {
            _characters = characters.ToList();
            var isSaved = await _platformService.SetInStorageAsync(StorageKeys.CurrentCharacterId, character.Id.ToString());
            _currentCharacter = character;

            return isSaved;
        }

        return false;
    }

}
