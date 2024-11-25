using Microsoft.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components.Cards;

public partial class CardBase
{
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public EventCallback EditCallback { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public bool HasValidationErrors { get; set; }

    [Parameter]
    public string EditButtonText { get; set; } = "Edit";
}