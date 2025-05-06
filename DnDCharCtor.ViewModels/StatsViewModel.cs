using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Common.Events;
using DnDCharCtor.Common.Services;
using DnDCharCtor.Common.Types;
using DnDCharCtor.Models;
using DnDCharCtor.ViewModels.ModelViewModels;
using System.ComponentModel;

namespace DnDCharCtor.ViewModels;

public class StatsViewModel : ObservableObject, IDisposable
{
    private readonly CharacterViewModel _characterViewModel;
    private readonly IStatsService _statsService;

    private ReEvaluatingLazy<Properties> LazyProperties => new(_characterViewModel.PropertiesViewModel.ToProperties);
    private ReEvaluatingLazy<RescueDices> LazyRescueDices => new(_characterViewModel.RescueDicesViewModel.ToRescueDices);


    public StatsViewModel(CharacterViewModel characterViewModel, IStatsService statsService)
    {
        _characterViewModel = characterViewModel;
        _statsService = statsService;
        _characterViewModel.PropertiesViewModel.PropertyChanged += NotifyStatChanged;
        _statsService.StatModifierChanged += OnStatModifierChanged;
    }

    public int Strength => _statsService.CalculateStat(LazyProperties.Value.Strength, nameof(Properties.Strength));
    public int Skillfulness => _statsService.CalculateStat(LazyProperties.Value.Skillfulness, nameof(Properties.Skillfulness));
    public int Constitution => _statsService.CalculateStat(LazyProperties.Value.Constitution, nameof(Properties.Constitution));
    public int Intelligence => _statsService.CalculateStat(LazyProperties.Value.Intelligence, nameof(Properties.Intelligence));
    public int Wisdom => _statsService.CalculateStat(LazyProperties.Value.Wisdom, nameof(Properties.Wisdom));
    public int Charisma => _statsService.CalculateStat(LazyProperties.Value.Charisma, nameof(Properties.Charisma));
    public int Inspiration => _statsService.CalculateStat(LazyProperties.Value.Inspiration, nameof(Properties.Inspiration));
    public int TrainingBonus => _statsService.CalculateStat(LazyProperties.Value.TrainingBonus, nameof(Properties.TrainingBonus));
    public int PassiveWisdomRecognition => _statsService.CalculateStat(LazyProperties.Value.PassiveWisdomRecognition, nameof(Properties.PassiveWisdomRecognition));

    public int GetStatWithTrainingBonus(string propertyName)
    {
        var baseStat = propertyName switch
        {
            nameof(Properties.Strength) => Strength,
            nameof(Properties.Skillfulness) => Skillfulness,
            nameof(Properties.Constitution) => Constitution,
            nameof(Properties.Intelligence) => Intelligence,
            nameof(Properties.Wisdom) => Wisdom,
            nameof(Properties.Charisma) => Charisma,
            _ => 0
        };

        var rescueDice = LazyRescueDices.Value;
        var hasTrainingBonus = propertyName switch
        {
            nameof(Properties.Strength) => rescueDice.Strength,
            nameof(Properties.Skillfulness) => rescueDice.Skillfulness,
            nameof(Properties.Constitution) => rescueDice.Constitution,
            nameof(Properties.Intelligence) => rescueDice.Intelligence,
            nameof(Properties.Wisdom) => rescueDice.Wisdom,
            nameof(Properties.Charisma) => rescueDice.Charisma,
            _ => false
        };

        return hasTrainingBonus ? baseStat + TrainingBonus : baseStat;
    }


    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _characterViewModel.PropertiesViewModel.PropertyChanged -= NotifyStatChanged;
        _statsService.StatModifierChanged -= OnStatModifierChanged;
    }



    private void OnStatModifierChanged(object? sender, StatModifierChangedEventArgs e)
    {
        LazyProperties.ReEvaluate();
        LazyRescueDices.ReEvaluate();

        foreach (var statName in e.AffectedStats)
        {
            OnPropertyChanged(statName);
        }
    }

    private void NotifyStatChanged(object? sender, PropertyChangedEventArgs e)
    {
        LazyProperties.ReEvaluate();
        LazyRescueDices.ReEvaluate();

        // Since the Property-Names of "StatsViewModel", "Properties", "PropertiesViewModel" & Co have the same "nameof" we can directly use their PropertyName - even when they are from different classes (as long as the properties have the same name).
        OnPropertyChanged(e.PropertyName);
    }
}
