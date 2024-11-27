using DnDCharCtor.Common.Validation.Attributes;
using DnDCharCtor.Resources;
using System.ComponentModel.DataAnnotations;

namespace DnDCharCtor.Models;

public record Personality
{
    [LocalizedRequired(nameof(StringResources.Character_Name))]
    [MaxLength(64)]
    public required string CharacterName { get; init; } = string.Empty;

    [LocalizedRequired(nameof(StringResources.Character_Personality_Class))]
    [MaxLength(32)]
    public required string ClassName { get; init; } = string.Empty;

    [LocalizedRequired(nameof(StringResources.Character_Personality_Level))]
    [Range(1, int.MaxValue)]
    public required int Level { get; init; }

    [MaxLength(1024)]
    public required string Background { get; init; } = string.Empty;

    [LocalizedRequired(nameof(StringResources.Character_Personality_PlayerName))]
    [MaxLength(64)]
    public required string PlayerName { get; init; } = string.Empty;

    [LocalizedRequired(nameof(StringResources.Character_Personality_Race))]
    [MaxLength(32)]
    public required string Race { get; init; } = string.Empty;

    [MaxLength(32)]
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
