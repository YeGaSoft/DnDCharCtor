using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels.ModelViewModels;

public partial class CharacterViewModel : ObservableValidator
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


    public bool Validate()
    {
        HasValidationErrors = PersonalityViewModel.Validate();

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
