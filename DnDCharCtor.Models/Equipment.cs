namespace DnDCharCtor.Models.Equipment;

public abstract class Equipment
{
    protected readonly List<IStatModifierCommand> _commands = new();
    public string Name { get; }
    public Guid Id { get; } = Guid.NewGuid();

    protected Equipment(string name)
    {
        Name = name;
    }

    public IReadOnlyList<IStatModifierCommand> Commands => _commands.AsReadOnly();
}
