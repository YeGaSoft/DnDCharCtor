using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components.Dialogs;

public partial class EditPersonalityDialog : IDisposable
{
    [CascadingParameter]
    public FluentDialog Dialog { get; set; } = default!;

    [Parameter]
    public PersonalityViewModel Content { get; set; } = default!;

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
            
        var isValid = _editContext.Validate();
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);
        
        _dataAnnotationValidator?.Dispose();
    }
}