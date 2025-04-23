using DnDCharCtor.Models;
using DnDCharCtor.Models.Equipment;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DnDCharCtor.Common.Services;

public class StatsService : INotifyPropertyChanged
{
    private readonly Properties _baseStats;
    private readonly HashSet<Equipment> _equipment = [];
    
    public event PropertyChangedEventHandler? PropertyChanged;

    public StatsService(Properties baseStats)
    {
        _baseStats = baseStats;
    }

    public bool AddEquipment(Equipment equipment)
    {
        if (_equipment.Any(e => e.GetType() == equipment.GetType()))
            return false;

        _equipment.Add(equipment);
        NotifyStatsChanged(equipment.Commands);
        return true;
    }

    public bool RemoveEquipment(Equipment equipment)
    {
        if (_equipment.Remove(equipment))
        {
            NotifyStatsChanged(equipment.Commands);
            return true;
        }
        return false;
    }

    private void NotifyStatsChanged(IEnumerable<IStatModifier> statModifiers)
    {
        var affectedStats = statModifiers.Select(c => c.StatName).Distinct();
        foreach (var stat in affectedStats)
        {
            OnPropertyChanged(stat);
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public int Strength => CalculateStat(_baseStats.Strength, nameof(Properties.Strength));
    public int Skillfulness => CalculateStat(_baseStats.Skillfulness, nameof(Properties.Skillfulness));
    public int Constitution => CalculateStat(_baseStats.Constitution, nameof(Properties.Constitution));
    public int Intelligence => CalculateStat(_baseStats.Intelligence, nameof(Properties.Intelligence));
    public int Wisdom => CalculateStat(_baseStats.Wisdom, nameof(Properties.Wisdom));
    public int Charisma => CalculateStat(_baseStats.Charisma, nameof(Properties.Charisma));
    public int Inspiration => CalculateStat(_baseStats.Inspiration, nameof(Properties.Inspiration));
    public int TrainingBonus => CalculateStat(_baseStats.TrainingBonus, nameof(Properties.TrainingBonus));
    public int PassiveWisdomRecognition => CalculateStat(_baseStats.PassiveWisdomRecognition, nameof(Properties.PassiveWisdomRecognition));

    private int CalculateStat(int baseStat, string statName)
    {
        var delta = _equipment
            .SelectMany(e => e.Commands)
            .Where(c => c.StatName == statName)
            .Sum(c => c.CalculateDelta(baseStat));

        return baseStat + delta;
    }
}
