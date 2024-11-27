using DnDCharCtor.Common.Extensions;
using DnDCharCtor.Common.Services;
using DnDCharCtor.Models;
using DnDCharCtor.Ui.Constants;
using DnDCharCtor.ViewModels.ModelViewModels;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DnDCharCtor.Ui.Layout;

public partial class NavMenu : IDisposable
{
    [Inject]
    public ILocalizationService LocalizationService { get; set; } = default!;

    [CascadingParameter(Name = CascadeValueNames.DataContext)]
    public INotifyPropertyChanged? DataContext { get; set; }

    // ToDo: reconsider this approach since its non-conform and makes Two-Way-Binding overcomplicated
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

        LocalizationService.PropertyChanged += LocalizationService_PropertyChanged;

        base.OnInitialized();
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);

        if (DataContext is not null)
        {
            DataContext.PropertyChanged -= DataContext_PropertyChanged;
        }

        LocalizationService.PropertyChanged -= LocalizationService_PropertyChanged;
    }



    private void DataContext_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (CurrentCharacterExpression is null) return;
        if (e.PropertyName != _expressionMemberName) return;

        _currentCharacterViewModel = CurrentCharacterExpression.Compile().Invoke();
        InvokeAsync(StateHasChanged).SafeFireAndForget(null);
    }

    private void LocalizationService_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged).SafeFireAndForget(null);
    }
}