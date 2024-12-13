using Microsoft.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components;

public partial class DebouncedSearch
{
    [Parameter]
    [EditorRequired]
    public string SearchText { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<string> SearchTextChanged { get; set; }

    [Parameter]
    public string? PlaceHolder { get; set; }

    [Parameter]
    public string? Label { get; set; }

    [Parameter]
    public string Style { get; set; } = string.Empty;

    private System.Timers.Timer? timer = null;
    private int _interval = 500;
    private int _keyPresses = 0;
    private string? _immediateSearchText = string.Empty;
    public string? DebouncingSearchText
    {
        get => _immediateSearchText;
        set
        {
            if (value == _immediateSearchText) return;

            _immediateSearchText = value;
            DisposeTimer();
            if (_interval > 0)
            {
                timer = new System.Timers.Timer(_interval);
                timer.Elapsed -= TimerElapsed_TickAsync;
                timer.Elapsed += TimerElapsed_TickAsync;
                timer.Enabled = true;
                timer.Start();
                DecreaseDebounce();
            }
            else
            {
                TimerElapsed_TickAsync(null, EventArgs.Empty);
            }
        }
    }
    private void DecreaseDebounce()
    {
        int reduceDebounceBy = (int)(50 * Math.Pow(1.1, _keyPresses));
        if (_interval - reduceDebounceBy > 0) _interval -= reduceDebounceBy;
        else _interval = 0;
        _keyPresses++;

    }
    private async void TimerElapsed_TickAsync(object? sender, EventArgs e)
    {
        _interval = 500;
        _keyPresses = 0;

        DisposeTimer();
        SearchText = _immediateSearchText ?? string.Empty;
        await InvokeAsync(async () => await SearchTextChanged.InvokeAsync(SearchText));
    }
    private void DisposeTimer()
    {
        if (timer is null) return;

        timer.Enabled = false;
        timer.Elapsed -= TimerElapsed_TickAsync;
        timer.Dispose();
        timer = null;
    }
}