using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace DnDCharCtor.Ui.Components.Input;

public partial class FluentIntegerField
{
    [Parameter]
    [EditorRequired]
    public int Value { get; set; }

    [Parameter]
    public EventCallback<int> ValueChanged { get; set; }

    [Parameter]
    [EditorRequired]
    public string Label { get; set; } = string.Empty;

    [Parameter]
    public bool Immediate { get; set; } = true;

    [Parameter]
    public bool Required { get; set; } = false;

    public string? InputValue { get; set; }

    private async Task InputChanged(string input)
    {
        if (InputValue == input) return;

        if (string.IsNullOrEmpty(input))
        {
            InputValue = string.Empty;
            if (Value is not 0)
            {
                Value = 0;
                await InvokeAsync(StateHasChanged);
            }

            return;
        }

        if (input == "-")
        {
            InputValue = input;
            await InvokeAsync(StateHasChanged);
            return;
        }

        if (int.TryParse(input, out var value))
        {
            if (Value == value) return;
            InputValue = value.ToString();
            Value = value;
            await ValueChanged.InvokeAsync(value);
        }
        else
        {
            // Ignore non-numeric input
            InputValue = Value.ToString();
            await InvokeAsync(StateHasChanged);
        }
    }

    protected override void OnParametersSet()
    {
        var valueStr = Value.ToString();
        if (InputValue != valueStr) InputValue = valueStr;

        base.OnParametersSet();
    }
}
