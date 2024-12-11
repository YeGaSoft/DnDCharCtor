using DnDCharCtor.Resources;
using DnDCharCtor.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Models;

public class Properties
{
    [LocalizedRange(nameof(StringResources.Character_Properties_Strength), 1, int.MaxValue)]
    public required int Strength { get; set; }

    [LocalizedRange(nameof(StringResources.Character_Properties_Skill), 1, int.MaxValue)]
    public required int _skill { get; set; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_Constitution))]
    [LocalizedRange(nameof(StringResources.Character_Properties_Constitution), 1, int.MaxValue)]
    public required int _constitution { get; set; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_Intelligence))]
    [LocalizedRange(nameof(StringResources.Character_Properties_Intelligence), 1, int.MaxValue)]
    public required int _intelligence { get; set; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_Wisdom))]
    [LocalizedRange(nameof(StringResources.Character_Properties_Wisdom), 1, int.MaxValue)]
    public required int _wisdom { get; set; }


    [LocalizedRequired(nameof(StringResources.Character_Properties_Charisma))]
    [LocalizedRange(nameof(StringResources.Character_Properties_Charisma), 1, int.MaxValue)]
    public required int _charisma { get; set; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_Inspiration))]
    [LocalizedRange(nameof(StringResources.Character_Properties_Inspiration), 1, int.MaxValue)]
    public required int _inspiration { get; set; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_TrainingBonus))]
    [LocalizedRange(nameof(StringResources.Character_Properties_TrainingBonus), 1, int.MaxValue)]
    public required int _trainingBonus { get; set; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_PassiveWisdomRecognition))]
    [LocalizedRange(nameof(StringResources.Character_Properties_PassiveWisdomRecognition), 1, int.MaxValue)]
    public required int _passiveWisdomRecognition { get; set; }
}
