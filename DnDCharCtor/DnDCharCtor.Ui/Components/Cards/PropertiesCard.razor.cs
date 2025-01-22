using DnDCharCtor.Common.Services;
using DnDCharCtor.Resources;
using DnDCharCtor.Ui.Components.Dialogs;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components.Cards;

public partial class PropertiesCard : EditableCardAbstraction<PropertiesViewModel, EditPropertiesDialog>
{
    [Inject]
    public IDndRulesService DndRulesService { get; set; } = default!;

    public override string DialogTitle => StringResources.CharacterEditor_Properties_Edit;
}