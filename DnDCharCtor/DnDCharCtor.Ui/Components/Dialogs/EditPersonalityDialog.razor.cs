using DnDCharCtor.Common.Utils;
using DnDCharCtor.Resources;
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

    protected override void OnInitialized()
    {
        // Customization:
        // You can localize this component by customizing the content of ChildContent,
        // but also the content of the progress area via the ProgressTemplate attribute.
        // The default progress area displays Loading, Completed or Canceled labels via static variables FluentInputFile.ResourceLoadingXXX.
        // These can be accessed to globally adapt the default display if you wish.
        FluentInputFile.ResourceLoadingBefore = StringResources.FileUpload_Loading;
        FluentInputFile.ResourceLoadingCompleted = StringResources.FileUpload_Completed;
        FluentInputFile.ResourceLoadingCanceled = StringResources.FileUpload_Cancelled;
        FluentInputFile.ResourceLoadingInProgress = StringResources.FileUpload_InProgress;
    }

    private async Task OnFileUploadedAsync(FluentInputFileEventArgs file)
    {
        if (file.Stream is null) return;
        Content.Base64EncodedImage = await ImageCompressor.CompressImageAndEncodeToBase64Async(file.Stream);
        await file.Stream!.DisposeAsync();

        await InvokeAsync(StateHasChanged);
    }
}