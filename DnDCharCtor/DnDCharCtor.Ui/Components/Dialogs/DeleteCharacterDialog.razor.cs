using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components.Dialogs;

public partial class DeleteCharacterDialog
{
    [CascadingParameter]
    public FluentDialog Dialog { get; set; } = default!;

    [Parameter]
    public CharacterViewModel Character { get; set; } = default!;
}