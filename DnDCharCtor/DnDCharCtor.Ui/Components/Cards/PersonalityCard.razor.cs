using DnDCharCtor.Common.Extensions;
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

public partial class PersonalityCard : IDisposable
{
    [Inject]
    public IDialogService DialogService { get; set; } = default!;

    [CascadingParameter(Name = CascadeValueNames.DataContext)]
    public INotifyPropertyChanged? DataContext { get; set; }

    private string _expressionMemberName = string.Empty;
    [Parameter]
    public Expression<Func<PersonalityViewModel?>>? PersonalityExpression { get; set; }
    private PersonalityViewModel? _personalityViewModel;

    private readonly DialogParameters _dialogParameters = new()
    {
        Title = $"Edit Personality",
        //Width = "500px",
        PreventDismissOnOverlayClick = true,
        ShowDismiss = true,
    };


    private async Task EditPersonality()
    {
        if (_personalityViewModel is null) return;

        var data = new PersonalityViewModel(_personalityViewModel);
        var dialog = await DialogService.ShowDialogAsync<EditPersonalityDialog>(data, _dialogParameters);
        var result = await dialog.Result;
        if (result.Cancelled is false && result.Data is not null)
        {
            _personalityViewModel = (PersonalityViewModel)result.Data;
            StateHasChanged();
        }
    }



    protected override void OnInitialized()
    {
        if (PersonalityExpression is not null)
        {
            _personalityViewModel = PersonalityExpression.Compile().Invoke();
            var memberExpression = (MemberExpression)PersonalityExpression.Body;
            _expressionMemberName = memberExpression.Member.Name;
        }

        if (DataContext is not null)
        {
            DataContext.PropertyChanged += DataContext_PropertyChanged;
        }

        base.OnInitialized();
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        if (DataContext is not null)
        {
            DataContext.PropertyChanged -= DataContext_PropertyChanged;
        }
    }



    private void DataContext_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (PersonalityExpression is null) return;
        if (e.PropertyName != _expressionMemberName) return;

        _personalityViewModel = PersonalityExpression.Compile().Invoke();
        InvokeAsync(StateHasChanged).SafeFireAndForget(null);
    }
}