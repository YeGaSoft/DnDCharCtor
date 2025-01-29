namespace DnDCharCtor.Common.Services;

public class DndRulesService : IDndRulesService
{
    public int CalculateStatModifier(int abilityScore)
    {
        if (abilityScore < 1) return 0;

        return (abilityScore - 10) / 2;
    }
}
