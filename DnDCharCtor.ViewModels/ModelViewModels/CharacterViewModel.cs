using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Common.Resources;
using DnDCharCtor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels.ModelViewModels;

public partial class CharacterViewModel : ObservableValidator, IValidateableViewModel
{
    public CharacterViewModel(Character character)
    {
        PersonalityViewModel = new(character.Personality);
    }

    public CharacterViewModel(CharacterViewModel characterViewModel)
    {
        PersonalityViewModel = new(characterViewModel.PersonalityViewModel);
    }

    [ObservableProperty]
    private PersonalityViewModel _personalityViewModel;

    [ObservableProperty]
    private bool _hasValidationErrors;
    public Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; } = [];
    public string ValidationErrorSource => StringResources.Character_Name;


    public bool Validate()
    {
        HasValidationErrors = PersonalityViewModel.Validate();
        ValidationErrors = ValidationErrors.Concat(PersonalityViewModel.ValidationErrors).ToDictionary();
        return HasValidationErrors is false;
    }


    public Character ToCharacter()
    {
        return new Character
        {
            Personality = PersonalityViewModel.ToPersonality(),
        };
    }
}
