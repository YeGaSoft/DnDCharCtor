namespace DnDCharCtor.Models;

public interface IStatModifierCommand
{
    int CalculateDelta(int baseValue);
    string StatName { get; }
}
