namespace DnDCharCtor.Models.Equipment;

public abstract class Equipment
{
    protected readonly List<IStatModifier> _commands = new();
    public string Name { get; }
    public Guid Id { get; } = Guid.NewGuid();

    protected Equipment(string name)
    {
        Name = name;
    }

    public IReadOnlyList<IStatModifier> Commands => _commands.AsReadOnly();
}
