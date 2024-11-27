using DnDCharCtor.Resources;
using DnDCharCtor.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DnDCharCtor.Models;

public record Personality
{
    [LocalizedRequired(nameof(StringResources.Character_Name))]
    [LocalizedMaxLength(nameof(StringResources.Character_Name), 64)]
    public required string CharacterName { get; init; } = string.Empty;

    [LocalizedRequired(nameof(StringResources.Character_Personality_Class))]
    [LocalizedMaxLength(nameof(StringResources.Character_Personality_Class), 32)]
    public required string ClassName { get; init; } = string.Empty;

    [LocalizedRequired(nameof(StringResources.Character_Personality_Level))]
    [Range(1, int.MaxValue)]
    public required int Level { get; init; }

    [LocalizedMaxLength(nameof(StringResources.Character_Personality_Background), 1024)]
    public required string Background { get; init; } = string.Empty;

    [LocalizedRequired(nameof(StringResources.Character_Personality_PlayerName))]
    [LocalizedMaxLength(nameof(StringResources.Character_Personality_PlayerName), 64)]
    public required string PlayerName { get; init; } = string.Empty;

    [LocalizedRequired(nameof(StringResources.Character_Personality_Race))]
    [LocalizedMaxLength(nameof(StringResources.Character_Personality_Race), 32)]
    public required string Race { get; init; } = string.Empty;

    [LocalizedMaxLength(nameof(StringResources.Character_Personality_Attitute), 32)]
    public required string Attitute { get; init; } = string.Empty;

    [LocalizedRequired(nameof(StringResources.Character_Personality_Experience))]
    [Range(0, int.MaxValue)]
    public required int Experience { get; init; }

    public static Personality Empty => new()
    {
        CharacterName = string.Empty,
        ClassName = string.Empty,
        Level = 0,
        Background = string.Empty,
        PlayerName = string.Empty,
        Race = string.Empty,
        Attitute = string.Empty,
        Experience = 0,
    };
}
