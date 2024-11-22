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

    [ObservableProperty]
    public CharacterViewModel _characterViewModelToEdit = new(Character.Empty);

    public async Task<bool> InitializeAsync(Guid characterId)
    {
        var characters = await _hybridCacheService.GetCharactersAsync();
        var existingCharacter = characters.FirstOrDefault(c => c.Id == characterId);
        CharacterViewModelToEdit = new(existingCharacter ?? Character.Empty);
        _characterViewModelBackup = new(CharacterViewModelToEdit); 
        return true;
    }

    public bool Initialize(Character character)
    {
        CharacterViewModelToEdit = new(character);
        _characterViewModelBackup = new(CharacterViewModelToEdit);

        return true;
    }

    public bool Initialize(CharacterViewModel characterViewModel)
    {
        CharacterViewModelToEdit = characterViewModel;
        _characterViewModelBackup = new(CharacterViewModelToEdit);

        return true;
    }
}
