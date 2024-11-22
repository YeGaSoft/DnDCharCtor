using DnDCharCtor.ViewModels;

namespace DnDCharCtor.Ui.Layout;

public partial class MainLayout
{
    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeAsync();
        await base.OnInitializedAsync();
    }
}