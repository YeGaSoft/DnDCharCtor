namespace DnDCharCtor.Common.Services;

public class DndRulesService : IDndRulesService
{
    public int CalculateStatModifier(int abilityScore)
    {
        return (abilityScore - 10) / 2;
    }
}
