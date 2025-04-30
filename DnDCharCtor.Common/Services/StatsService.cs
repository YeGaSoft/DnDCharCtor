using System.ComponentModel;
using System.Runtime.CompilerServices;
using DnDCharCtor.Common.Events;
using DnDCharCtor.Models;

namespace DnDCharCtor.Common.Services;

public class StatsService : IStatsService, IDisposable
{
    private readonly IDndRulesService _dndRulesService;
    private readonly IEquipmentService _equipmentService;

    public StatsService(IDndRulesService dndRulesService, IEquipmentService equipmentService)
    {
        _dndRulesService = dndRulesService;
        _equipmentService = equipmentService;
        _equipmentService.EquipmentChanged += OnEquipmentChanged;
    }

    public event EventHandler<StatModifierChangedEventArgs>? StatModifierChanged;

   
    public int CalculateStat(int abilityScore, string statName)
    {
        // First calculate the DnD rules-based modifier from the ability score.
        var baseModifier = _dndRulesService.CalculateStatModifier(abilityScore);

        // Then apply any equipment modifiers to the base modifier.
        var equipmentModifiers = _equipmentService.GetModifiersForStat(statName);
        var equipmentBonus = equipmentModifiers.Sum(m => m.CalculateDelta(baseModifier));

        return baseModifier + equipmentBonus;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _equipmentService.EquipmentChanged -= OnEquipmentChanged;
    }

    private void OnEquipmentChanged(object? sender, EquipmentChangedEventArgs e)
    {
        var affectedStats = e.Equipment.Commands.Select(c => c.StatName).Distinct();
        StatModifierChanged?.Invoke(this, new StatModifierChangedEventArgs(affectedStats));
    }
}
