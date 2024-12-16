using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Resources;
using DnDCharCtor.Models;
using DnDCharCtor.Ui.Components.Dialogs;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DnDCharCtor.Ui.Components.Cards;

public partial class PersonalityCard : EditableCardAbstraction<PersonalityViewModel, EditPersonalityDialog>
{
    public override string DialogTitle => StringResources.CharacterEditor_Personality_Edit;
}