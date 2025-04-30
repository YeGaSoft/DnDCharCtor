using DnDCharCtor.Common.Events;

namespace DnDCharCtor.Common.Services;

public interface IStatsService
{
    event EventHandler<StatModifierChangedEventArgs> StatModifierChanged;

    /// <summary>
    /// Calculates the final stat value by applying DnD rules and equipment modifiers.
    /// </summary>
    /// <param name="abilityScore">The raw ability score (typically 1-30 from character creation or leveling).</param>
    /// <param name="statName">The name of the stat being calculated (e.g. "Strength", "Dexterity").</param>
    /// <returns>
    /// The final calculated stat value after applying:
    /// <br />1. DnD ability score to modifier conversion
    /// <br />2. Equipment bonuses/penalties
    /// </returns>
    int CalculateStat(int abilityScore, string statName);
}
