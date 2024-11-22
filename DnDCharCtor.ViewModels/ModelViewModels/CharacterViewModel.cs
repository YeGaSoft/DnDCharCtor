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
        this.PersonalityViewModel = new(character.Personality);
    }

    [ObservableProperty]
    public PersonalityViewModel _personalityViewModel;

    public Character ToCharacter()
    {
        return new Character
        {
            Personality = PersonalityViewModel.ToPersonality(),
        };
    }
}
