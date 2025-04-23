using System.ComponentModel;
using System.Runtime.CompilerServices;
using DnDCharCtor.Common.Events;
using DnDCharCtor.Models;

namespace DnDCharCtor.Common.Services;

public class StatsService : IStatsService, IDisposable
{
    private readonly IEquipmentService _equipmentService;
    
    public StatsService(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
        _equipmentService.EquipmentChanged += OnEquipmentChanged;
    }

    public event EventHandler<StatModifierChangedEventArgs>? StatModifierChanged;

    public int CalculateStat(int baseStat, string statName)
    {
        var modifiers = _equipmentService.GetModifiersForStat(statName);
        var delta = modifiers.Sum(m => m.CalculateDelta(baseStat));
        
        return baseStat + delta;
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
