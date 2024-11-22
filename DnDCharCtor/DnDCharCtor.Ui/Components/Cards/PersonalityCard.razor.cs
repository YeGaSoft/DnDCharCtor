using DnDCharCtor.Models;
using DnDCharCtor.Ui.Components.Dialogs;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DnDCharCtor.Ui.Components.Cards;

public partial class PersonalityCard
{
    [CascadingParameter(Name = CascadeValueNames.DataContext)]
    public INotifyPropertyChanged? DataContext { get; set; }

    private string _expressionMemberName = string.Empty;
    [Parameter]
    public Expression<Func<Personality?>>? PersonalityExpression { get; set; }

    private DialogParameters _dialogParameters = new()
    {
        Title = $"Edit Personality",
        //Width = "500px",
        PreventDismissOnOverlayClick = true,
        PreventScroll = true,
    };


    private async void EditPersonalityViewModel()
    {
        var data = new PersonalityViewModel(PersonalityViewModel);

        var dialog = await DialogService.ShowDialogAsync<EditPersonalityDialog>(PersonalityViewModel, _dialogParameters);
        var result = await dialog.Result;
        if (result.Cancelled is false && result.Data is not null)
        {
            PersonalityViewModel = (PersonalityViewModel)result.Data;
            StateHasChanged();
        }
    }
}