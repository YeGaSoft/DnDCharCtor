using System.ComponentModel.DataAnnotations;

namespace DnDCharCtor.Models;

public record Personality
{
    [Required]
    [MaxLength(64)]
    public required string CharacterName { get; init; } = string.Empty;

    [Required]
    [MaxLength(32)]
    public required string ClassName { get; init; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue)]
    public required int Level { get; init; }

    [MaxLength(1024)]
    public required string Background { get; init; } = string.Empty;

    [Required]
    [MaxLength(64)]
    public required string PlayerName { get; init; } = string.Empty;

    [Required]
    [MaxLength(32)]
    public required string Race { get; init; } = string.Empty;

    [MaxLength(32)]
    public required string Attitute { get; init; } = string.Empty;

    [Required]
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
