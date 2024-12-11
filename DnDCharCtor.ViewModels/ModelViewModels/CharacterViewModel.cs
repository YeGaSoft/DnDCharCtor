using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Resources;
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
        CharacterId = character.Id;
        PersonalityViewModel = new(character.Personality);
        PropertiesViewModel = new(character.Properties);
    }

    public CharacterViewModel(CharacterViewModel characterViewModel)
    {
        CharacterId = characterViewModel.CharacterId;
        PersonalityViewModel = new(characterViewModel.PersonalityViewModel);
        PropertiesViewModel = new(characterViewModel.PropertiesViewModel);
    }

    public Guid CharacterId { get; set; }

    [ObservableProperty]
    private PersonalityViewModel _personalityViewModel;

    [ObservableProperty]
    private PropertiesViewModel _propertiesViewModel;

    [ObservableProperty]
    private bool _hasValidationErrors;
    public Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; } = [];
    public string ValidationErrorSource => StringResources.Character_Name;


    public bool Validate()
    {
        HasValidationErrors = PersonalityViewModel.Validate();
        ValidationErrors.Clear();
        ValidationErrors = ValidationErrors.Concat(PersonalityViewModel.ValidationErrors).ToDictionary();
        return HasValidationErrors is false;
    }


    public Character ToCharacter()
    {
        return new Character
        {
            Id = CharacterId,
            Personality = PersonalityViewModel.ToPersonality(),
            Properties = PropertiesViewModel.ToProperties(),
        };
    }
}
