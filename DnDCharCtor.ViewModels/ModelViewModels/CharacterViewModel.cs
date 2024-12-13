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

public partial class CharacterViewModel : ObservableValidator, IValidateAndCopyableViewModel<CharacterViewModel>
{
    public CharacterViewModel(Character character)
    {
        CharacterId = character.Id;
        PersonalityViewModel = new(character.Personality);
        PropertiesViewModel = new(character.Properties);
        RescueDicesViewModel = new(character.RescueDices);
    }

    public CharacterViewModel(CharacterViewModel characterViewModel)
    {
        CharacterId = characterViewModel.CharacterId;
        PersonalityViewModel = new(characterViewModel.PersonalityViewModel);
        PropertiesViewModel = new(characterViewModel.PropertiesViewModel);
        RescueDicesViewModel = new(characterViewModel.RescueDicesViewModel);
    }

    public Guid CharacterId { get; set; }

    [ObservableProperty]
    private PersonalityViewModel _personalityViewModel;

    [ObservableProperty]
    private PropertiesViewModel _propertiesViewModel;

    [ObservableProperty]
    private RescueDicesViewModel _rescueDicesViewModel;

    [ObservableProperty]
    private bool _hasValidationErrors;
    public Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; } = [];
    public string ValidationErrorSource => StringResources.Character_Name;


    public bool Validate()
    {
        HasValidationErrors =
            PersonalityViewModel.Validate()
            && PropertiesViewModel.Validate()
            && RescueDicesViewModel.Validate();
        ValidationErrors.Clear();
        ValidationErrors = 
            ValidationErrors
            .Concat(PersonalityViewModel.ValidationErrors)
            .Concat(PropertiesViewModel.ValidationErrors)
            .Concat(RescueDicesViewModel.ValidationErrors)
            .Where(validationErrorSource => validationErrorSource.Value.Count > 0)
            .ToDictionary();
        return HasValidationErrors is false;
    }

    public CharacterViewModel CreateShallowCopy()
    {
        return new CharacterViewModel(this);
    }


    public Character ToCharacter()
    {
        return new Character
        {
            Id = CharacterId,
            Personality = PersonalityViewModel.ToPersonality(),
            Properties = PropertiesViewModel.ToProperties(),
            RescueDices = RescueDicesViewModel.ToRescueDices(),
        };
    }
}
