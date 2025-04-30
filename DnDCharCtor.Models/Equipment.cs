namespace DnDCharCtor.Models;

public abstract class Equipment
{
    public string Name { get; }
    public Guid Id { get; } = Guid.NewGuid();

    protected Equipment(string name)
    {
        Name = name;
    }

    private readonly List<IStatModifier> _commands = [];

    public IReadOnlyList<IStatModifier> Commands => _commands.AsReadOnly();
}
