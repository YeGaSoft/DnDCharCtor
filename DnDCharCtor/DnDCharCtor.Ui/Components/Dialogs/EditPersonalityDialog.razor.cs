using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DnDCharCtor.Ui.Components.Dialogs;

public partial class EditPersonalityDialog
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; } = default!;

    [CascadingParameter]
    public FluentDialog Dialog { get; set; } = default!;

    [Parameter]
    public PersonalityViewModel Content { get; set; } = default!;
}