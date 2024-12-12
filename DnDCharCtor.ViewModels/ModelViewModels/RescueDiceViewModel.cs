using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Models;
using DnDCharCtor.Resources;
using DnDCharCtor.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels.ModelViewModels;

public partial class RescueDicesViewModel : ObservableValidator, IViewModelBase<RescueDicesViewModel>
{
    public RescueDicesViewModel(RescueDices rescueDices)
    {
        Strength = rescueDices.Strength.ToString();
        Skill = rescueDices.Skill.ToString();
        Constitution = rescueDices.Constitution.ToString();
        Intelligence = rescueDices.Intelligence.ToString();
        Wisdom = rescueDices.Wisdom.ToString();
        Charisma = rescueDices.Charisma.ToString();
    }

    public RescueDicesViewModel(RescueDicesViewModel rescueDicesViewModel)
    {
        Strength = rescueDicesViewModel.Strength;
        Skill = rescueDicesViewModel.Skill;
        Constitution = rescueDicesViewModel.Constitution;
        Intelligence = rescueDicesViewModel.Intelligence;
        Wisdom = rescueDicesViewModel.Wisdom;
        Charisma = rescueDicesViewModel.Charisma;

        HasValidationErrors = rescueDicesViewModel.HasValidationErrors;
        if (rescueDicesViewModel.HasValidationErrors) Validate();
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
    private bool _hasValidationErrors;
    public Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; } = [];
    public string ValidationErrorSource => StringResources.Character_RescueDices;


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

    public RescueDicesViewModel CreateShallowCopy()
    {
        return new RescueDicesViewModel(this);
    }


    public RescueDices ToRescueDices()
    {
        var hasStrength = int.TryParse(Strength, out var strength);
        var hasSkill = int.TryParse(Skill, out var skill);
        var hasConstitution = int.TryParse(Constitution, out var constitution);
        var hasIntelligence = int.TryParse(Intelligence, out var intelligence);
        var hasWisdom = int.TryParse(Wisdom, out var wisdom);
        var hasCharisma = int.TryParse(Charisma, out var charisma);

        return new RescueDices
        {
            Strength = hasStrength ? strength : 0,
            Skill = hasSkill ? skill : 0,
            Constitution = hasConstitution ? constitution : 0,
            Intelligence = hasIntelligence ? intelligence : 0,
            Wisdom = hasWisdom ? wisdom : 0,
            Charisma = hasCharisma ? charisma : 0,
        };
    }
}
