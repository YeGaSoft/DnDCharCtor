namespace DnDCharCtor.Models;

public interface IStatModifier
{
    int CalculateDelta(int baseValue);
    string StatName { get; }
}
