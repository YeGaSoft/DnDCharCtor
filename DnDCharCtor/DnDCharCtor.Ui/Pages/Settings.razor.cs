
namespace DnDCharCtor.Ui.Pages;

public partial class Settings
{
    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeAsync();

        await base.OnInitializedAsync();
    }
}