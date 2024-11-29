using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DnDCharCtor.Resources;
using DnDCharCtor.Validation.Attributes;

namespace DnDCharCtor.Models;

public record Character
{
    public Guid Id { get; init; } = Guid.NewGuid();

    [LocalizedRequired(nameof(StringResources.Character_Personality))]
    public required Personality Personality { get; init; } = Personality.Empty;

    public static Character Empty => new ()
    {
        Personality = Personality.Empty,
    };
}
