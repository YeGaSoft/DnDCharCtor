using DnDCharCtor.Common.Events;
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
        ViewModel.PropertyChanged += PropertyChanged;
        
        // We do the Log here since in Program.cs it seems the log won't be applied.
        Console.WriteLine($"In the Logs you might see errors like 'Failed to fetch'{Environment.NewLine}" +
            $"but these errors only occur when there is no internet connection.{Environment.NewLine}" +
            $"This is NOT a problem, since the components are already loaded.{Environment.NewLine}" +
            $"It only tries to load newer files.");

        await InvokeAsync(StateHasChanged);
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        ViewModel.PropertyChanged -= PropertyChanged;
    }



    private void PropertyChanged(object? sender, EventArgs e)
    {
        InvokeAsync(StateHasChanged).SafeFireAndForget(null);
    }
}