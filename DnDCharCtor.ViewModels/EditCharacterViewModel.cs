using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Common.Events;
using DnDCharCtor.Common.Services;
using DnDCharCtor.Models;
using DnDCharCtor.Resources;
using DnDCharCtor.ViewModels.ModelViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels;

public partial class EditCharacterViewModel : ObservableValidator, IValidateableViewModel, IDisposable
{
    private readonly IHybridCacheService _hybridCacheService;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILocalizationService _localizationService;

    public EditCharacterViewModel(IHybridCacheService hybridCacheService, IEventAggregator eventAggregator, ILocalizationService localizationService)
    {
        _hybridCacheService = hybridCacheService;
        _eventAggregator = eventAggregator;
        _localizationService = localizationService;
        _localizationService.PropertyChanged += LocalizationService_OnPropertyChanged;
    }

    private CharacterViewModel _characterViewModelBackup = new(Character.Empty);

    [ObservableProperty]
    private CharacterViewModel _characterViewModelToEdit = new(Character.Empty);

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private bool _hasValidationErrors;
    public Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; } = [];
    public string ValidationErrorSource => CharacterViewModelToEdit.ValidationErrorSource;

    public async Task<bool> InitializeAsync(Guid characterId)
    {
        var characters = await _hybridCacheService.GetCharactersAsync();
        var existingCharacter = characters.FirstOrDefault(c => c.Id == characterId);
        var characterToEdit = new CharacterViewModel(existingCharacter ?? Character.Empty);
        return Initialize(characterToEdit);
    }

    public bool Initialize(Character character)
    {
        var characterToEdit = new CharacterViewModel(character);
        return Initialize(characterToEdit);
    }

    public bool Initialize(CharacterViewModel characterViewModel)
    {
        CharacterViewModelToEdit = characterViewModel;
        _characterViewModelBackup = new(CharacterViewModelToEdit);

        UpdateTitle();
        
        return true;
    }

    private void UpdateTitle()
    {
        var characterName = CharacterViewModelToEdit.PersonalityViewModel.CharacterName;
        var hasName = string.IsNullOrWhiteSpace(characterName) is false;
        Title = hasName ? string.Format(StringResources.CharacterEditor_Edit, characterName) : StringResources.CharacterEditor_Create;
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



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _localizationService.PropertyChanged -= LocalizationService_OnPropertyChanged;
    }



    private void LocalizationService_OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        UpdateTitle();
    }
}
