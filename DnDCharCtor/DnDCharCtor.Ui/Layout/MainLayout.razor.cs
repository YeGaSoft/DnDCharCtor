using DnDCharCtor.ViewModels;

namespace DnDCharCtor.Ui.Layout;

public partial class MainLayout : IDisposable
{
    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeAsync();
        ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        
        await base.OnInitializedAsync();
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
    }



    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }
}