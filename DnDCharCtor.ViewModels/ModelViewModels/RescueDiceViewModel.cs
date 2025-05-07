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
    private bool _strength;

    [ObservableProperty]
    private bool _skillfulness;

    [ObservableProperty]
    private bool _constitution;

    [ObservableProperty]
    private bool _intelligence;

    [ObservableProperty]
    private bool _wisdom;

    [ObservableProperty]
    private bool _charisma;

    [ObservableProperty]
    private bool _hasValidationErrors;
    public Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; } = [];
    public string ValidationErrorSource => StringResources.Character_RescueDices;


    public bool Validate()
    {
        // There are no required fields
        return true;
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

        // Since all properties are boolean, we can ignore them in the search

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
