using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Models;
using DnDCharCtor.Resources;
using DnDCharCtor.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels.ModelViewModels;

public partial class PropertiesViewModel : ObservableValidator, IViewModelBase<PropertiesViewModel>
{
    public PropertiesViewModel(Properties properties)
    {
        Strength = properties.Strength.ToString();
        Skill = properties.Skill.ToString();
        Constitution = properties.Constitution.ToString();
        Intelligence = properties.Intelligence.ToString();
        Wisdom = properties.Wisdom.ToString();
        Charisma = properties.Charisma.ToString();
        Inspiration = properties.Inspiration.ToString();
        TrainingBonus = properties.TrainingBonus.ToString();
        PassiveWisdomRecognition = properties.PassiveWisdomRecognition.ToString();
    }

    public PropertiesViewModel(PropertiesViewModel propertiesViewModel)
    {
        Strength = propertiesViewModel.Strength;
        Skill = propertiesViewModel.Skill;
        Constitution = propertiesViewModel.Constitution;
        Intelligence = propertiesViewModel.Intelligence;
        Wisdom = propertiesViewModel.Wisdom;
        Charisma = propertiesViewModel.Charisma;
        Inspiration = propertiesViewModel.Inspiration;
        TrainingBonus = propertiesViewModel.TrainingBonus;
        PassiveWisdomRecognition = propertiesViewModel.PassiveWisdomRecognition;

        HasValidationErrors = propertiesViewModel.HasValidationErrors;
        if (propertiesViewModel.HasValidationErrors) Validate();
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Strength))]
    [LocalizedParsedRange(nameof(StringResources.Character_Strength), 1, int.MaxValue)]
    private string _strength;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skill))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skill), 1, int.MaxValue)]
    private string _skill;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Constitution))]
    [LocalizedParsedRange(nameof(StringResources.Character_Constitution), 1, int.MaxValue)]
    private string _constitution;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Intelligence))]
    [LocalizedParsedRange(nameof(StringResources.Character_Intelligence), 1, int.MaxValue)]
    private string _intelligence;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Wisdom))]
    [LocalizedParsedRange(nameof(StringResources.Character_Wisdom), 1, int.MaxValue)]
    private string _wisdom;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Charisma))]
    [LocalizedParsedRange(nameof(StringResources.Character_Charisma), 1, int.MaxValue)]
    private string _charisma;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Inspiration))]
    [LocalizedParsedRange(nameof(StringResources.Character_Inspiration), 1, int.MaxValue)]
    private string _inspiration;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_TrainingBonus))]
    [LocalizedParsedRange(nameof(StringResources.Character_TrainingBonus), 1, int.MaxValue)]
    private string _trainingBonus;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_PassiveWisdomRecognition))]
    [LocalizedParsedRange(nameof(StringResources.Character_PassiveWisdomRecognition), 1, int.MaxValue)]
    private string _passiveWisdomRecognition;


    [ObservableProperty]
    private bool _hasValidationErrors;
    public Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; } = [];
    public string ValidationErrorSource => StringResources.Character_Properties;


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

    public PropertiesViewModel CreateShallowCopy()
    {
        return new PropertiesViewModel(this);
    }

    public bool Search(string searchText, bool includePropertyNames)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return true;
        }

        // Check string properties
        if (Strength.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Skill.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Constitution.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Intelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Wisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Charisma.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Inspiration.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            TrainingBonus.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            PassiveWisdomRecognition.Contains(searchText, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (includePropertyNames is false) return false;

        // Check string resources
        if (StringResources.Character_Properties.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Strength.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skill.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Constitution.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Intelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Wisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Charisma.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Inspiration.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_TrainingBonus.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_PassiveWisdomRecognition.Contains(searchText, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }


    public Properties ToProperties()
    {
        var hasStrength = int.TryParse(Strength, out var strength);
        var hasSkill = int.TryParse(Skill, out var skill);
        var hasConstitution = int.TryParse(Constitution, out var constitution);
        var hasIntelligence = int.TryParse(Intelligence, out var intelligence);
        var hasWisdom = int.TryParse(Wisdom, out var wisdom);
        var hasCharisma = int.TryParse(Charisma, out var charisma);
        var hasInspiration = int.TryParse(Inspiration, out var inspiration);
        var hasTrainingBonus = int.TryParse(TrainingBonus, out var trainingBonus);
        var hasPassiveWisdomRecognition = int.TryParse(PassiveWisdomRecognition, out var passiveWisdomRecognition);

        return new Properties
        {
            Strength = hasStrength ? strength : 0,
            Skill = hasSkill ? skill : 0,
            Constitution = hasConstitution ? constitution : 0,
            Intelligence = hasIntelligence ? intelligence : 0,
            Wisdom = hasWisdom ? wisdom : 0,
            Charisma = hasCharisma ? charisma : 0,
            Inspiration = hasInspiration ? inspiration : 0,
            TrainingBonus = hasTrainingBonus ? trainingBonus : 0,
            PassiveWisdomRecognition = hasPassiveWisdomRecognition ? passiveWisdomRecognition : 0,
        };
    }
}
