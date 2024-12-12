using DnDCharCtor.Resources;
using DnDCharCtor.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Models;

public record Properties
{
    [LocalizedRequired(nameof(StringResources.Character_Properties_Strength))]
    [LocalizedRange(nameof(StringResources.Character_Properties_Strength), 1, int.MaxValue)]
    public required int Strength { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_Skill))]
    [LocalizedRange(nameof(StringResources.Character_Properties_Skill), 1, int.MaxValue)]
    public required int Skill { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_Constitution))]
    [LocalizedRange(nameof(StringResources.Character_Properties_Constitution), 1, int.MaxValue)]
    public required int Constitution { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_Intelligence))]
    [LocalizedRange(nameof(StringResources.Character_Properties_Intelligence), 1, int.MaxValue)]
    public required int Intelligence { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_Wisdom))]
    [LocalizedRange(nameof(StringResources.Character_Properties_Wisdom), 1, int.MaxValue)]
    public required int Wisdom { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_Charisma))]
    [LocalizedRange(nameof(StringResources.Character_Properties_Charisma), 1, int.MaxValue)]
    public required int Charisma { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_Inspiration))]
    [LocalizedRange(nameof(StringResources.Character_Properties_Inspiration), 1, int.MaxValue)]
    public required int Inspiration { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_TrainingBonus))]
    [LocalizedRange(nameof(StringResources.Character_Properties_TrainingBonus), 1, int.MaxValue)]
    public required int TrainingBonus { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Properties_PassiveWisdomRecognition))]
    [LocalizedRange(nameof(StringResources.Character_Properties_PassiveWisdomRecognition), 1, int.MaxValue)]
    public required int PassiveWisdomRecognition { get; init; }

    public static Properties Empty => new()
    { 
        Strength = 0,
        Skill = 0,
        Constitution = 0,
        Intelligence = 0,
        Wisdom = 0,
        Charisma = 0, 
        Inspiration = 0, 
        TrainingBonus = 0, 
        PassiveWisdomRecognition = 0 };
}
