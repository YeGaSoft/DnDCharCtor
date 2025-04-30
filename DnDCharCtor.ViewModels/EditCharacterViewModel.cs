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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels;

public partial class EditCharacterViewModel : ObservableValidator, IValidateable, IDisposable
{
    private readonly IHybridCacheService _hybridCacheService;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILocalizationService _localizationService;
    private readonly IDndRulesService _dndRulesService;

    public EditCharacterViewModel(IHybridCacheService hybridCacheService, IEventAggregator eventAggregator, ILocalizationService localizationService, IDndRulesService dndRulesService)
    {
        _hybridCacheService = hybridCacheService;
        _eventAggregator = eventAggregator;
        _localizationService = localizationService;
        _localizationService.PropertyChanged += LocalizationService_OnPropertyChanged;
        _dndRulesService = dndRulesService;

        _characterViewModelBackup = new(Character.Empty, _dndRulesService);
        CharacterViewModelToEdit = new(Character.Empty, _dndRulesService);
    }

    private CharacterViewModel _characterViewModelBackup;

    [ObservableProperty]
    private CharacterViewModel _characterViewModelToEdit;

    [ObservableProperty]
    private string _title = StringResources.CharacterEditor_Create;
    public EditMode EditMode { get; private set; } = EditMode.Create;
    public bool IsSaved { get; private set; } = false;

    [ObservableProperty]
    private bool _hasValidationErrors;
    public Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; } = [];
    public string ValidationErrorSource => CharacterViewModelToEdit.ValidationErrorSource;

    public async Task<bool> InitializeAsync(Guid characterId)
    {
        var characters = await _hybridCacheService.GetCharactersAsync();
        var existingCharacter = characters.FirstOrDefault(c => c.Id == characterId);
        var characterToEdit = new CharacterViewModel(existingCharacter ?? Character.Empty, _dndRulesService);
        return Initialize(characterToEdit, EditMode.Edit);
    }

    public bool Initialize(Character character, EditMode editMode = EditMode.Edit)
    {
        var characterToEdit = new CharacterViewModel(character, _dndRulesService);
        return Initialize(characterToEdit, editMode);
    }

    public bool Initialize(CharacterViewModel characterViewModel, EditMode editMode = EditMode.Edit)
    {
        CharacterViewModelToEdit = characterViewModel;
        _characterViewModelBackup = new(CharacterViewModelToEdit, _dndRulesService);

        IsSaved = false;

        UpdateTitle();

        EditMode = editMode;
        _eventAggregator.GetEvent<EditModeChangedEvent>().Publish();

        return true;
    }

    private void UpdateTitle()
    {
        if (EditMode is EditMode.Create)
        {
            Title = StringResources.CharacterEditor_Create;
            return;
        }

        var characterName = CharacterViewModelToEdit.PersonalityViewModel.CharacterName;
        var hasName = string.IsNullOrWhiteSpace(characterName) is false;
        Title = hasName ? string.Format(CultureInfo.CurrentCulture, StringResources.CharacterEditor_Edit, characterName) : StringResources.CharacterEditor_Create;
    }


    public bool Validate()
    {
        HasValidationErrors = CharacterViewModelToEdit.Validate();
        ValidationErrors = CharacterViewModelToEdit.ValidationErrors;

        return HasValidationErrors is false;
    }

    public bool HasUnsavedChanges()
    {
        var hasChanges = IsDefault() is false && HasChanges();
        var isUnsaved = IsSaved is false;
        return hasChanges && isUnsaved;
    }

    public bool IsDefault()
    {
        var character = CharacterViewModelToEdit.ToCharacter();
        var isDefault = character == Character.Empty;
        return isDefault;
    }

    public bool HasChanges()
    {
        var isChanged = CharacterViewModelToEdit.ToCharacter() != _characterViewModelBackup.ToCharacter();
        return isChanged;
    }

    public bool Reset()
    {
        CharacterViewModelToEdit = new(_characterViewModelBackup, _dndRulesService);
        return true;
    }

    public async Task<bool> SaveAsync()
    {
        if (EditMode is EditMode.Create) CharacterViewModelToEdit.CharacterId = Guid.NewGuid();
        var characterToSave = CharacterViewModelToEdit.ToCharacter();
        IsSaved = await _hybridCacheService.SetCurrentCharacterAsync(characterToSave);
        if (IsSaved) _eventAggregator.GetEvent<CurrentCharacterChangedEvent>().Publish();
        return IsSaved;
    }



    ~EditCharacterViewModel() => Dispose();

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

public enum EditMode
{
    Create,
    Edit,
}
