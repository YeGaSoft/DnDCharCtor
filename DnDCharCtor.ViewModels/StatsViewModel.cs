using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Common.Events;
using DnDCharCtor.Common.Services;
using DnDCharCtor.Common.Types;
using DnDCharCtor.Models;
using DnDCharCtor.Models.Equipment;
using DnDCharCtor.ViewModels.ModelViewModels;
using System.ComponentModel;

namespace DnDCharCtor.ViewModels;

public class StatsViewModel : ObservableObject, IDisposable
{
    private readonly CharacterViewModel _characterViewModel;
    private readonly IStatsService _statsService;

    private ReEvaluatingLazy<Properties> LazyProperties => new(_characterViewModel.PropertiesViewModel.ToProperties);

    public StatsViewModel(CharacterViewModel characterViewModel, IStatsService statsService)
    {
        _characterViewModel = characterViewModel;
        _statsService = statsService;
        //_characterViewModel.PropertiesViewModel.PropertyChanged += NotifyStatChanged;
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



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _characterViewModel.PropertiesViewModel.PropertyChanged -= NotifyStatChanged;
        _statsService.StatModifierChanged -= OnStatModifierChanged;
    }



    private void OnStatModifierChanged(object? sender, StatModifierChangedEventArgs e)
    {
        LazyProperties.ReEvaluate();

        foreach (var statName in e.AffectedStats)
        {
            OnPropertyChanged(statName);
        }
    }

    private void NotifyStatChanged(object? sender, PropertyChangedEventArgs e)
    {
        LazyProperties.ReEvaluate();

        // Since the Property-Names of "StatsViewModel", "Properties", "PropertiesViewModel" & Co have the same "nameof" we can directly use their PropertyName - even when they are from different classes (as long as the properties have the same name).
        OnPropertyChanged(e.PropertyName);
    }
}
