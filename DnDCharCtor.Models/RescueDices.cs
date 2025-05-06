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
    public required bool Strength { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skillfulness))]
    public required bool Skillfulness { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Constitution))]
    public required bool Constitution { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Intelligence))]
    public required bool Intelligence { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Wisdom))]
    public required bool Wisdom { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Charisma))]
    public required bool Charisma { get; init; }

    public static RescueDices Empty => new()
    {
        Strength = false,
        Skillfulness = false,
        Constitution = false,
        Intelligence = false,
        Wisdom = false,
        Charisma = false,
    };
}
