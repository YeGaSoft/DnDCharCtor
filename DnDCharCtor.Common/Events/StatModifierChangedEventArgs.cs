namespace DnDCharCtor.Common.Events;

public class StatModifierChangedEventArgs : EventArgs
{
    public IReadOnlyList<string> AffectedStats { get; }
    
    public StatModifierChangedEventArgs(IEnumerable<string> affectedStats)
    {
        AffectedStats = [..affectedStats];
    }
}
