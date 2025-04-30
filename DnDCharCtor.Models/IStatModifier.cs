namespace DnDCharCtor.Models;

public interface IStatModifier
{
    int CalculateDelta(int abilityScore);
    string StatName { get; }
}
