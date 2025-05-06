using DnDCharCtor.Resources;
using DnDCharCtor.Ui.Components.Dialogs;
using DnDCharCtor.ViewModels;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components.Cards;

public partial class RescueDicesCard : EditableCardAbstraction<RescueDicesViewModel, EditRescueDicesDialog>
{
    [Parameter]
    [EditorRequired]
    public required StatsViewModel StatsVM { get; set; }

    public override string DialogTitle => StringResources.CharacterEditor_RescueDices_Edit;
}