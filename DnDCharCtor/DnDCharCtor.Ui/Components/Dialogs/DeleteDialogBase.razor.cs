using DnDCharCtor.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components.Dialogs;

public partial class DeleteDialogBase
{
    [CascadingParameter]
    public FluentDialog Dialog { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    [Parameter]
    public string DeleteButtonText { get; set; } = StringResources.Button_Delete;

    [Parameter]
    public string CancelButtonText { get; set; } = StringResources.Button_Cancel;

    private async Task ConfirmDeleteAsync()
    {
        await Dialog.CloseAsync();
    }

    private async Task CancelAsync()
    {
        await Dialog.CancelAsync();
    }
}