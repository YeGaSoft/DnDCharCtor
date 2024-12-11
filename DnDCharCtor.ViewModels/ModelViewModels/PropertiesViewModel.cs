using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Resources;
using DnDCharCtor.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels.ModelViewModels;

public partial class PropertiesViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Properties_Strength))]
    [LocalizedParsedRange(nameof(StringResources.Character_Properties_Strength), 1, int.MaxValue)]
    private string _strength;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Properties_Skill))]
    [LocalizedParsedRange(nameof(StringResources.Character_Properties_Skill), 1, int.MaxValue)]
    private string _skill;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Properties_Constitution))]
    [LocalizedParsedRange(nameof(StringResources.Character_Properties_Constitution), 1, int.MaxValue)]
    private string _constitution;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Properties_Intelligence))]
    [LocalizedParsedRange(nameof(StringResources.Character_Properties_Intelligence), 1, int.MaxValue)]
    private string _intelligence;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Properties_Wisdom))]
    [LocalizedParsedRange(nameof(StringResources.Character_Properties_Wisdom), 1, int.MaxValue)]
    private string _wisdom;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Properties_Charisma))]
    [LocalizedParsedRange(nameof(StringResources.Character_Properties_Charisma), 1, int.MaxValue)]
    private string _charisma;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Properties_Inspiration))]
    [LocalizedParsedRange(nameof(StringResources.Character_Properties_Inspiration), 1, int.MaxValue)]
    private string _inspiration;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Properties_TrainingBonus))]
    [LocalizedParsedRange(nameof(StringResources.Character_Properties_TrainingBonus), 1, int.MaxValue)]
    private string _trainingBonus;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Properties_PassiveWisdomRecognition))]
    [LocalizedParsedRange(nameof(StringResources.Character_Properties_PassiveWisdomRecognition), 1, int.MaxValue)]
    private string _passiveWisdomRecognition;
}
