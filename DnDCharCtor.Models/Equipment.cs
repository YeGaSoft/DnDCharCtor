namespace DnDCharCtor.Models.Equipment;

public abstract class Equipment
{
    private readonly List<IStatModifier> _commands = [];

    public string Name { get; }
    public Guid Id { get; } = Guid.NewGuid();

    protected Equipment(string name)
    {
        Name = name;
    }

    public IReadOnlyList<IStatModifier> Commands => _commands.AsReadOnly();
}
