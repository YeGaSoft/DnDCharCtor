using DnDCharCtor.Common.Events;
using DnDCharCtor.Models;

namespace DnDCharCtor.Common.Services;

public class EquipmentService : IEquipmentService
{
    private readonly HashSet<Equipment> _equipment = [];

    public event EventHandler<EquipmentChangedEventArgs>? EquipmentChanged;

    public bool CanApplyEquipment(Equipment equipment)
    {
        return _equipment.Any(e => e.GetType() == equipment.GetType()) is false;
    }

    public bool AddEquipment(Equipment equipment)
    {
        if (CanApplyEquipment(equipment) is false)
            return false;

        if (_equipment.Add(equipment))
        {
            EquipmentChanged?.Invoke(this, new EquipmentChangedEventArgs(equipment, EquipmentChangeType.Added));
            return true;
        }
        return false;
    }

    public bool RemoveEquipment(Equipment equipment)
    {
        if (_equipment.Remove(equipment))
        {
            EquipmentChanged?.Invoke(this, new EquipmentChangedEventArgs(equipment, EquipmentChangeType.Removed));
            return true;
        }
        return false;
    }

    public IReadOnlyList<Equipment> GetAppliedEquipment()
    {
        return _equipment.ToList().AsReadOnly();
    }

    public IEnumerable<IStatModifier> GetModifiersForStat(string statName)
    {
        return _equipment
            .SelectMany(e => e.Commands)
            .Where(c => c.StatName == statName);
    }
}
