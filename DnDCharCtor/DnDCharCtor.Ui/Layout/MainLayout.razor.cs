using DnDCharCtor.Common.Extensions;
using DnDCharCtor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace DnDCharCtor.Ui.Layout;

public partial class MainLayout : IDisposable
{
    [Inject]
    public MainViewModel ViewModel { get; set; } = default!;



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
        InvokeAsync(StateHasChanged).SafeFireAndForget(null);
    }
}