using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Models;

public record Character
{
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    public Personality? Personality { get; init; }
}

public record Personality
{
    [Required]
    [MaxLength(64)]
    public string CharacterName { get; init; } = string.Empty;

    [Required]
    [MaxLength(32)]
    public string ClassName { get; init; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue)]
    public int Level { get; init; }

    [MaxLength(1024)]
    public string Background { get; init; } = string.Empty;

    [Required]
    [MaxLength(64)]
    public string PlayerName { get; init; } = string.Empty;

    [Required]
    [MaxLength(32)]
    public string Race { get; init; } = string.Empty;

    [MaxLength(32)]
    public string Attitute { get; init; } = string.Empty;

    [Required]
    [Range(0, int.MaxValue)]
    public int Experience { get; init; }
}
