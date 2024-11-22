using DnDCharCtor.Models;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DnDCharCtor.Ui.Layout;

public partial class NavMenu : IDisposable
{
    [CascadingParameter(Name = CascadeValueNames.DataContext)]
    public INotifyPropertyChanged? DataContext { get; set; }

    private string _expressionMemberName = string.Empty;
    [Parameter]
    public Expression<Func<CharacterViewModel>>? CurrentCharacterExpression { get; set; }

    private CharacterViewModel? _currentCharacterViewModel;

    protected override void OnInitialized()
    {
        if (CurrentCharacterExpression is not null)
        {
            _currentCharacterViewModel = CurrentCharacterExpression.Compile().Invoke();
            var memberExpression = (MemberExpression)CurrentCharacterExpression.Body;
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
        if (CurrentCharacterExpression is null) return;
        if (e.PropertyName != _expressionMemberName) return;

        _currentCharacterViewModel = CurrentCharacterExpression.Compile().Invoke();
        InvokeAsync(StateHasChanged);
    }
}