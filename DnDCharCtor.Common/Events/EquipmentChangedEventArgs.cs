using DnDCharCtor.Models;

namespace DnDCharCtor.Common.Events;

public class EquipmentChangedEventArgs : EventArgs
{
    public Equipment Equipment { get; }
    public EquipmentChangeType ChangeType { get; }

    public EquipmentChangedEventArgs(Equipment equipment, EquipmentChangeType equipmentChangeType)
    {
        Equipment = equipment;
        ChangeType = equipmentChangeType;
    }
}

public enum EquipmentChangeType
{
    Added,
    Removed
}