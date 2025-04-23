using DnDCharCtor.Common.Events;

namespace DnDCharCtor.Common.Services;

public interface IStatsService
{
    event EventHandler<StatModifierChangedEventArgs> StatModifierChanged;

    int CalculateStat(int baseStat, string statName);
}
