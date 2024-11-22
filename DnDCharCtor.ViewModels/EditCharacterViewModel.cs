using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Common.Services;
using DnDCharCtor.Models;
using DnDCharCtor.ViewModels.ModelViewModels;
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

    private CharacterViewModel _characterViewModelBackup = new(Character.Empty);

    [Required]
    [ObservableProperty]
    public CharacterViewModel _editCharacterViewModel = new(Character.Empty);

    public async Task<bool> InitializeAsync(Guid characterId)
    {
        var characters = await _hybridCacheService.GetCharactersAsync();
        var existingCharacter = characters.FirstOrDefault(c => c.Id == characterId);
        EditCharacterViewModel = new(existingCharacter ?? Character.Empty);
        _characterViewModelBackup = new(EditCharacterViewModel); 
        return true;
    }

    public bool Initialize(Character character)
    {
        EditCharacterViewModel = new(character);
        _characterViewModelBackup = new(EditCharacterViewModel);

        return true;
    }

    public bool Initialize(CharacterViewModel characterViewModel)
    {
        EditCharacterViewModel = characterViewModel;
        _characterViewModelBackup = new(EditCharacterViewModel);

        return true;
    }
}
