using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Common.Types;
using DnDCharCtor.Models;
using DnDCharCtor.Models.Equipment;
using DnDCharCtor.ViewModels.ModelViewModels;
using System.ComponentModel;

namespace DnDCharCtor.ViewModels;

public class StatsViewModel : ObservableObject, IDisposable
{
    private readonly CharacterViewModel _characterViewModel;
    private readonly HashSet<Equipment> _equipment = [];

    private ReEvaluatingLazy<Properties> LazyProperties => new( _characterViewModel.PropertiesViewModel.ToProperties);

    public StatsViewModel(CharacterViewModel characterViewModel)
    {
        _characterViewModel = characterViewModel;

        _characterViewModel.PropertiesViewModel.PropertyChanged += NotifyStatChanged;
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

    public int Strength => CalculateStat(LazyProperties.Value.Strength, nameof(Properties.Strength));
    public int Skillfulness => CalculateStat(LazyProperties.Value.Skillfulness, nameof(Properties.Skillfulness));
    public int Constitution => CalculateStat(LazyProperties.Value.Constitution, nameof(Properties.Constitution));
    public int Intelligence => CalculateStat(LazyProperties.Value.Intelligence, nameof(Properties.Intelligence));
    public int Wisdom => CalculateStat(LazyProperties.Value.Wisdom, nameof(Properties.Wisdom));
    public int Charisma => CalculateStat(LazyProperties.Value.Charisma, nameof(Properties.Charisma));
    public int Inspiration => CalculateStat(LazyProperties.Value.Inspiration, nameof(Properties.Inspiration));
    public int TrainingBonus => CalculateStat(LazyProperties.Value.TrainingBonus, nameof(Properties.TrainingBonus));
    public int PassiveWisdomRecognition => CalculateStat(LazyProperties.Value.PassiveWisdomRecognition, nameof(Properties.PassiveWisdomRecognition));

    private int CalculateStat(int baseStat, string statName)
    {
        var delta = _equipment
            .SelectMany(e => e.Commands)
            .Where(c => c.StatName == statName)
            .Sum(c => c.CalculateDelta(baseStat));

        return baseStat + delta;
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _characterViewModel.PropertiesViewModel.PropertyChanged -= NotifyStatChanged;
    }



    private void NotifyStatChanged(object? sender, PropertyChangedEventArgs e)
    {
        LazyProperties.ReEvaluate();

        switch (e.PropertyName)
        {
            case nameof(PropertiesViewModel.Strength):
                OnPropertyChanged(nameof(Strength));
                break;
            case nameof(PropertiesViewModel.Skillfulness):
                OnPropertyChanged(nameof(Skillfulness));
                break;
            case nameof(PropertiesViewModel.Constitution):
                OnPropertyChanged(nameof(Constitution));
                break;
            case nameof(PropertiesViewModel.Intelligence):
                OnPropertyChanged(nameof(Intelligence));
                break;
            case nameof(PropertiesViewModel.Wisdom):
                OnPropertyChanged(nameof(Wisdom));
                break;
            case nameof(PropertiesViewModel.Charisma):
                OnPropertyChanged(nameof(Charisma));
                break;
            case nameof(PropertiesViewModel.Inspiration):
                OnPropertyChanged(nameof(Inspiration));
                break;
            case nameof(PropertiesViewModel.TrainingBonus):
                OnPropertyChanged(nameof(TrainingBonus));
                break;
            case nameof(PropertiesViewModel.PassiveWisdomRecognition):
                OnPropertyChanged(nameof(PassiveWisdomRecognition));
                break;
        }
    }
}
