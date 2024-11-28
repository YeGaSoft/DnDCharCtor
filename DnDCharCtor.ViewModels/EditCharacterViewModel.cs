using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Common.Events;
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

public partial class EditCharacterViewModel : ObservableValidator, IValidateableViewModel
{
    private readonly IHybridCacheService _hybridCacheService;
    private readonly IEventAggregator _eventAggregator;

    public EditCharacterViewModel(IHybridCacheService hybridCacheService, IEventAggregator eventAggregator)
    {
        _hybridCacheService = hybridCacheService;
        _eventAggregator = eventAggregator;
    }

    private CharacterViewModel _characterViewModelBackup = new(Character.Empty);

    [ObservableProperty]
    private CharacterViewModel _characterViewModelToEdit = new(Character.Empty);

    [ObservableProperty]
    private bool _hasValidationErrors;
    public Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; } = [];
    public string ValidationErrorSource => CharacterViewModelToEdit.ValidationErrorSource;

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


    public bool Validate()
    {
        HasValidationErrors = CharacterViewModelToEdit.Validate();
        ValidationErrors = CharacterViewModelToEdit.ValidationErrors;
        return HasValidationErrors is false;
    }

    public async Task<bool> SaveAsync()
    {
        var characterToSave = CharacterViewModelToEdit.ToCharacter();
        var isSaved = await _hybridCacheService.SetCurrentCharacterAsync(characterToSave);
        if (isSaved) _eventAggregator.GetEvent<CurrentCharacterChangedEvent>().Publish();
        return isSaved;
    }
}
