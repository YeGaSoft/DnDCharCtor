using DnDCharCtor.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DnDCharCtor.Ui.Components.Cards;

public partial class EditableCardBase
{
    [Parameter]
    [EditorRequired]
    public RenderFragment ChildContent { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public EventCallback EditButtonCallback { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public EventCallback CardClickCallback { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public bool HasValidationErrors { get; set; }


    [Parameter]
    public string EditButtonText { get; set; } = StringResources.Button_Edit;

    [Parameter]
    public Icon EditButtonIcon { get; set; } = new Icons.Regular.Size20.Edit();


    [Parameter]
    public bool ShowDeleteButton { get; set; } = false;

    [Parameter]
    public EventCallback DeleteButtonCallback { get; set; } = default!;

    [Parameter]
    public string DeleteButtonText { get; set; } = StringResources.Button_Delete;


    [Parameter]
    public bool ShowPrimaryButton { get; set; } = false;

    [Parameter]
    public EventCallback PrimaryButtonCallback { get; set; } = default!;

    [Parameter]
    public string PrimaryButtonText { get; set; } = StringResources.Button_Select;

    [Parameter]
    public Icon PrimaryButtonIcon { get; set; } = new Icons.Regular.Size20.AccessibilityCheckmark();
}