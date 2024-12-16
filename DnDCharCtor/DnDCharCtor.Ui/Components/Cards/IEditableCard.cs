
namespace DnDCharCtor.Ui.Components.Cards;

public interface IEditableCard
{
    string DialogTitle { get; }

    Task EditAsync();
}