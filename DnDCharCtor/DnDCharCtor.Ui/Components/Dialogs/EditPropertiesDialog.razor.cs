using DnDCharCtor.Common.Services;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components.Dialogs;

public partial class EditPropertiesDialog
{
    [CascadingParameter]
    public FluentDialog Dialog { get; set; } = default!;

    [Parameter]
    public EditDialogParameter<PropertiesViewModel> Content { get; set; } = default!;

    [Inject]
    public IDndRulesService DndRulesService { get; set; } = default!;
}