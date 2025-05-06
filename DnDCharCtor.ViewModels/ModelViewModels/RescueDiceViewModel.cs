using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Models;
using DnDCharCtor.Resources;
using DnDCharCtor.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels.ModelViewModels;

public partial class RescueDicesViewModel : ObservableValidator, IViewModelBase<RescueDicesViewModel>
{
    public RescueDicesViewModel(RescueDices rescueDices)
    {
        Strength = rescueDices.Strength;
        Skillfulness = rescueDices.Skillfulness;
        Constitution = rescueDices.Constitution;
        Intelligence = rescueDices.Intelligence;
        Wisdom = rescueDices.Wisdom;
        Charisma = rescueDices.Charisma;
    }

    public RescueDicesViewModel(RescueDicesViewModel rescueDicesViewModel)
    {
        Strength = rescueDicesViewModel.Strength;
        Skillfulness = rescueDicesViewModel.Skillfulness;
        Constitution = rescueDicesViewModel.Constitution;
        Intelligence = rescueDicesViewModel.Intelligence;
        Wisdom = rescueDicesViewModel.Wisdom;
        Charisma = rescueDicesViewModel.Charisma;

        HasValidationErrors = rescueDicesViewModel.HasValidationErrors;
        if (rescueDicesViewModel.HasValidationErrors) Validate();
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Strength))]
    private bool _strength;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Skillfulness))]
    private bool _skillfulness;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Constitution))]
    private bool _constitution;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Intelligence))]
    private bool _intelligence;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Wisdom))]
    private bool _wisdom;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedRequired(nameof(StringResources.Character_Charisma))]
    private bool _charisma;

    [ObservableProperty]
    private bool _hasValidationErrors;
    public Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; } = [];
    public string ValidationErrorSource => StringResources.Character_RescueDices;


    public bool Validate()
    {
        ClearErrors(null);
        ValidateAllProperties();

        var validationContext = new ValidationContext(this);
        var validationResults = new List<ValidationResult>();

        HasValidationErrors = Validator.TryValidateObject(this, validationContext, validationResults, true) is false;
        ValidationErrors.Clear();
        ValidationErrors[ValidationErrorSource] = validationResults;
        return HasValidationErrors is false;
    }

    public RescueDicesViewModel CreateShallowCopy()
    {
        return new RescueDicesViewModel(this);
    }

    public bool Search(string searchText, bool includePropertyNames)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return true;
        }

        // Check string properties
        if (Strength.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Skillfulness.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Constitution.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Intelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Wisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            Charisma.Contains(searchText, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (includePropertyNames is false) return false;

        // Check string resources
        if (StringResources.Character_RescueDices.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Strength.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skillfulness.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Constitution.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Intelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Wisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Charisma.Contains(searchText, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }


    public RescueDices ToRescueDices()
    {
        return new RescueDices
        {
            Strength = Strength,
            Skillfulness = Skillfulness,
            Constitution = Constitution,
            Intelligence = Intelligence,
            Wisdom = Wisdom,
            Charisma = Charisma,
        };
    }
}
