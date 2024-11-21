using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Common.Services;
using DnDCharCtor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels;

public partial class EditCharacterViewModel : ObservableValidator
{
    private readonly IHybridCacheService _hybridCacheService;

    public EditCharacterViewModel(IHybridCacheService hybridCacheService)
    {
        _hybridCacheService = hybridCacheService;
    }

    private Character _characterBackup = new();

    [Required]
    [ObservableProperty]
    public Character _editCharacter = new();

    public async Task<bool> InitializeAsync(Guid characterId)
    {
        var characters = await _hybridCacheService.GetCharactersAsync();
        var existingCharacter = characters.FirstOrDefault(c => c.Id == characterId);
        EditCharacter = existingCharacter ?? new();
        _characterBackup = EditCharacter with { }; 
        return true;
    }

    public bool Initialize(Character character)
    {
        EditCharacter = character;
        _characterBackup = EditCharacter with { };

        return true;
    }
}
