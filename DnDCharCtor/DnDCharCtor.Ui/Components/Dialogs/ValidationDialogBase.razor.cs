using DnDCharCtor.Resources;
using DnDCharCtor.ViewModels;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components.Dialogs;

public partial class ValidationDialogBase : ComponentBase, IDisposable
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
    public string SubmitButtonText { get; set; } = StringResources.Button_Submit;

    private EditContext _editContext = default!;

    private async Task CloseDialogAsync()
    {
        await Dialog.CloseAsync(Content);
    }


    private IDisposable? _dataAnnotationValidator;
    protected override void OnInitialized()
    {
        _editContext = new EditContext(Content);
        _dataAnnotationValidator = _editContext.EnableDataAnnotationsValidation(ServiceProvider);
        if (Content.HasValidationErrors is false) return;

        _editContext.Validate();
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _dataAnnotationValidator?.Dispose();
    }
}