using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Fast.Components.FluentUI;

namespace DnDCharCtor.Ui.Components.Dialogs;

public partial class EditPersonalityDialog
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



    protected override void OnInitialized()
    {
        _editContext = new EditContext(Content);
    }
}