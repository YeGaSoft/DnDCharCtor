using DnDCharCtor.Resources;
using DnDCharCtor.Ui.Components.Dialogs;
using DnDCharCtor.ViewModels.ModelViewModels;

namespace DnDCharCtor.Ui.Components.Cards;

public partial class RescueDicesCard : EditableCardAbstraction<RescueDicesViewModel, EditRescueDicesDialog>
{
    public override string DialogTitle => StringResources.CharacterEditor_RescueDices_Edit;
}