using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Resources;
using DnDCharCtor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DnDCharCtor.Validation.Attributes;

namespace DnDCharCtor.ViewModels.ModelViewModels;

public partial class PersonalityViewModel : ObservableValidator, IViewModelBase<PersonalityViewModel>
{
    public PersonalityViewModel(Personality personality)
    {
        CharacterName = personality.CharacterName;
        Base64EncodedImage = personality.Base64EncodedImage;
        ClassName = personality.ClassName;
        Level = personality.Level.ToString();
        Background = personality.Background;
        PlayerName = personality.PlayerName;
        Race = personality.Race;
        Attitude = personality.Attitude;
        Experience = personality.Experience.ToString();
    }

    public PersonalityViewModel(PersonalityViewModel personalityViewModel)
    {
        CharacterName = personalityViewModel.CharacterName;
        Base64EncodedImage = personalityViewModel.Base64EncodedImage;
        ClassName = personalityViewModel.ClassName;
        Level = personalityViewModel.Level;
        Background = personalityViewModel.Background;
        PlayerName = personalityViewModel.PlayerName;
        Race = personalityViewModel.Race;
        Attitude = personalityViewModel.Attitude;
        Experience = personalityViewModel.Experience;

        HasValidationErrors = personalityViewModel.HasValidationErrors;
        if (personalityViewModel.HasValidationErrors) Validate();
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Name))]
    [LocalizedMaxLength(nameof(StringResources.Character_Name), 64)]
    private string _characterName;

    [ObservableProperty]
    private string? _base64EncodedImage;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Personality_Class))]
    [LocalizedMaxLength(nameof(StringResources.Character_Personality_Class), 32)]
    private string _className;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Personality_Level))]
    [LocalizedParsedRange(nameof(StringResources.Character_Personality_Level), 1, int.MaxValue)]
    private string _level;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedMaxLength(nameof(StringResources.Character_Personality_Background), 1024)]
    private string _background;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Personality_PlayerName))]
    [LocalizedMaxLength(nameof(StringResources.Character_Personality_PlayerName), 64)]
    private string _playerName;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Personality_Race))]
    [LocalizedMaxLength(nameof(StringResources.Character_Personality_Race), 32)]
    private string _race;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedMaxLength(nameof(StringResources.Character_Personality_Attitude), 32)]
    private string _Attitude;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Personality_Experience))]
    [LocalizedParsedRange(nameof(StringResources.Character_Personality_Experience), 0, int.MaxValue)]
    private string _experience;


    [ObservableProperty]
    private bool _hasValidationErrors;
    public Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; } = [];
    public string ValidationErrorSource => StringResources.Character_Personality;


    public bool Validate()
    {
        ClearErrors(null);
        ValidateAllProperties();

        var validationContext = new ValidationContext(this);
        var validationResults = new List<ValidationResult>();

        HasValidationErrors = Validator.TryValidateObject(this, validationContext, validationResults, true) is false;
        ValidationErrors.Clear();
        ValidationErrors[ValidationErrorSource] = validationResults;
        return HasValidationErrors is false;
    }

    public PersonalityViewModel CreateShallowCopy()
    {
        return new PersonalityViewModel(this);
    }

    public bool Search(string searchText, bool includePropertyNames)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return true;
        }

        // Check string properties
        if (CharacterName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            ClassName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Level.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Background.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            PlayerName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Race.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Attitude.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Experience.Contains(searchText, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (includePropertyNames is false) return false;

        // Check string resources
        if (StringResources.Character_Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Personality.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Personality_Class.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Personality_Level.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Personality_Background.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Personality_PlayerName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Personality_Race.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Personality_Attitude.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Personality_Experience.Contains(searchText, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }



    public Personality ToPersonality()
    {
        var hasLevel = int.TryParse(Level, out var level);
        var hasExperience = int.TryParse(Experience, out var experience);

        return new Personality
        {
            CharacterName = CharacterName,
            Base64EncodedImage = Base64EncodedImage,
            ClassName = ClassName,
            Level = hasLevel ? level : 0,
            Background = Background,
            PlayerName = PlayerName,
            Race = Race,
            Attitude = Attitude,
            Experience = hasExperience ? experience : 0,
        };
    }
}
