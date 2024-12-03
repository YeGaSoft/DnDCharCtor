using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components.Dialogs;

public partial class UnsavedChangedDialog
{
    [CascadingParameter]
    public FluentDialog Dialog { get; set; } = default!;
}