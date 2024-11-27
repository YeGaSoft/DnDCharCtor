using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Resources;
using DnDCharCtor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DnDCharCtor.Common.Validation.Attributes;

namespace DnDCharCtor.ViewModels.ModelViewModels;

public partial class PersonalityViewModel : ObservableValidator, IValidateableViewModel
{
    public PersonalityViewModel(Personality personality)
    {
        CharacterName = personality.CharacterName;
        ClassName = personality.ClassName;
        Level = personality.Level;
        Background = personality.Background;
        PlayerName = personality.PlayerName;
        Race = personality.Race;
        Attitute = personality.Attitute;
        Experience = personality.Experience;
    }

    public PersonalityViewModel(PersonalityViewModel personalityViewModel)
    {
        CharacterName = personalityViewModel.CharacterName;
        ClassName = personalityViewModel.ClassName;
        Level = personalityViewModel.Level;
        Background = personalityViewModel.Background;
        PlayerName = personalityViewModel.PlayerName;
        Race = personalityViewModel.Race;
        Attitute = personalityViewModel.Attitute;
        Experience = personalityViewModel.Experience;

        HasValidationErrors = personalityViewModel.HasValidationErrors;
        if (personalityViewModel.HasValidationErrors) Validate();
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Name))]
    [MaxLength(64)]
    private string _characterName;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Personality_Class))]
    [MaxLength(32)]
    private string _className;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Personality_Level))]
    [Range(1, int.MaxValue)]
    private int _level;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MaxLength(1024)]
    private string _background;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Personality_PlayerName))]
    [MaxLength(64)]
    private string _playerName;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Personality_Race))]
    [MaxLength(32)]
    private string _race;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MaxLength(32)]
    private string _attitute;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Personality_Experience))]
    [Range(0, int.MaxValue)]
    private int _experience;

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


    public Personality ToPersonality()
    {
        return new Personality
        {
            CharacterName = CharacterName,
            ClassName = ClassName,
            Level = Level,
            Background = Background,
            PlayerName = PlayerName,
            Race = Race,
            Attitute = Attitute,
            Experience = Experience,
        };
    }
}
