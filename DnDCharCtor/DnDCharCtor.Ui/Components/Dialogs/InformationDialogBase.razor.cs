using DnDCharCtor.Resources;
using DnDCharCtor.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components.Dialogs;

public partial class InformationDialogBase : ComponentBase
{
    [Inject]
    public IServiceProvider ServiceProvider { get; set; } = default!;

    [CascadingParameter]
    public FluentDialog Dialog { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public IValidateable Content { get; set; } = default!;

    [Parameter]
    public string CloseButtonText { get; set; } = StringResources.Button_Ok;

    private async Task CloseDialogAsync()
    {
        await Dialog.CloseAsync(Content);
    }
}