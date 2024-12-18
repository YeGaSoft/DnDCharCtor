using DnDCharCtor.Resources;
using DnDCharCtor.Ui.Components.Dialogs;
using DnDCharCtor.ViewModels.ModelViewModels;

namespace DnDCharCtor.Ui.Components.Cards;

public partial class SkillsCard : EditableCardAbstraction<SkillsViewModel, EditSkillsDialog>
{
    public override string DialogTitle => StringResources.CharacterEditor_Skills_Edit;
}