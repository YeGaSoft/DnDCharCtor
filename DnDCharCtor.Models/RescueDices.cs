using DnDCharCtor.Resources;
using DnDCharCtor.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Models;

public record RescueDices
{
    [LocalizedRequired(nameof(StringResources.Character_Strength))]
    [LocalizedRange(nameof(StringResources.Character_Strength), 1, int.MaxValue)]
    public required int Strength { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skill))]
    [LocalizedRange(nameof(StringResources.Character_Skill), 1, int.MaxValue)]
    public required int Skill { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Constitution))]
    [LocalizedRange(nameof(StringResources.Character_Constitution), 1, int.MaxValue)]
    public required int Constitution { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Intelligence))]
    [LocalizedRange(nameof(StringResources.Character_Intelligence), 1, int.MaxValue)]
    public required int Intelligence { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Wisdom))]
    [LocalizedRange(nameof(StringResources.Character_Wisdom), 1, int.MaxValue)]
    public required int Wisdom { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Charisma))]
    [LocalizedRange(nameof(StringResources.Character_Charisma), 1, int.MaxValue)]
    public required int Charisma { get; init; }

    public static RescueDices Empty => new()
    {
        Strength = 0,
        Skill = 0,
        Constitution = 0,
        Intelligence = 0,
        Wisdom = 0,
        Charisma = 0,
    };
}
