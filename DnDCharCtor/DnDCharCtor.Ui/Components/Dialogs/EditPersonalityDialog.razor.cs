using DnDCharCtor.Common.Utils;
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

    private async Task OnFileUploadedAsync(FluentInputFileEventArgs file)
    {
        if (file.Stream is null) return;
        Content.Base64EncodedImage = await ImageCompressor.CompressImageAndEncodeToBase64Async(file.Stream);
        await file.Stream!.DisposeAsync();

        await InvokeAsync(StateHasChanged);
    }
}