using DnDCharCtor.Common.Events;
using DnDCharCtor.Models;

namespace DnDCharCtor.Common.Services;

public interface IEquipmentService
{
    event EventHandler<EquipmentChangedEventArgs> EquipmentChanged;

    bool CanApplyEquipment(Equipment equipment);
    bool AddEquipment(Equipment equipment);
    bool RemoveEquipment(Equipment equipment);
    IReadOnlyList<Equipment> GetAppliedEquipment();
    IEnumerable<IStatModifier> GetModifiersForStat(string statName);
}
