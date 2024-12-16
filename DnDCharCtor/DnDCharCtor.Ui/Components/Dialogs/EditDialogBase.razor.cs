using DnDCharCtor.Resources;
using DnDCharCtor.ViewModels;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components.Dialogs;

public partial class EditDialogBase<TValidateable> : ComponentBase, IDisposable
    where TValidateable : IValidateable
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
    public EditDialogParameter<TValidateable> Content { get; set; } = default!;

    [Parameter]
    public string SubmitButtonText { get; set; } = StringResources.Button_Submit;

    private EditContext _editContext = default!;



    private async Task CloseDialogAsync()
    {
        await Dialog.CloseAsync(Content);
    }

    private async Task CloseDialogAndEditPreviousAsync()
    {
        await Dialog.CloseAsync(Content);

        if (Content.PreviousCard is null) return;
        await Content.PreviousCard.EditAsync();
    }

    private async Task CloseDialogAndEditNextAsync()
    {
        await Dialog.CloseAsync(Content);

        if (Content.NextCard is null) return;
        await Content.NextCard.EditAsync();
    }


    private IDisposable? _dataAnnotationValidator;
    protected override void OnInitialized()
    {
        _editContext = new EditContext(Content.ViewModel);
        _dataAnnotationValidator = _editContext.EnableDataAnnotationsValidation(ServiceProvider);
        if (Content.ViewModel.HasValidationErrors is false) return;

        _editContext.Validate();
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _dataAnnotationValidator?.Dispose();
    }
}