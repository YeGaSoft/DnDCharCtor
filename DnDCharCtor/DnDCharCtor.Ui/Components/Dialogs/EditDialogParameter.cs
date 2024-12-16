using DnDCharCtor.Ui.Components.Cards;
using DnDCharCtor.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Ui.Components.Dialogs;

public record class EditDialogParameter<TValidateable>
    where TValidateable : IValidateable
{
    public IEditableCard? PreviousCard { get; set; }
    public required TValidateable ViewModel { get; set; }
    public IEditableCard? NextCard { get; set; }
}
