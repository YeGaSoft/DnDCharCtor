using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels.ModelViewModels;

public partial class PersonalityViewModel : ObservableValidator
{
    public PersonalityViewModel(Personality personality)
    {
        CharacterName = personality.CharacterName;
        ClassName = personality.ClassName;
        Level = personality.Level;
        Background = personality.Background;
        PlayerName = personality.PlayerName;
        Race = personality.Race;
        Attitute = personality.Attitute;
        Experience = personality.Experience;
    }

    public PersonalityViewModel(PersonalityViewModel personalityViewModel)
    {
        CharacterName = personalityViewModel.CharacterName;
        ClassName = personalityViewModel.ClassName;
        Level = personalityViewModel.Level;
        Background = personalityViewModel.Background;
        PlayerName = personalityViewModel.PlayerName;
        Race = personalityViewModel.Race;
        Attitute = personalityViewModel.Attitute;
        Experience = personalityViewModel.Experience;
    }

    [Required]
    [MaxLength(64)]
    public string CharacterName { get; set; }

    [Required]
    [MaxLength(32)]
    public string ClassName { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Level { get; set; }

    [MaxLength(1024)]
    public string Background { get; set; }

    [Required]
    [MaxLength(64)]
    public string PlayerName { get; set; }

    [Required]
    [MaxLength(32)]
    public string Race { get; set; }

    [MaxLength(32)]
    public string Attitute { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Experience { get; set; }

    public Personality ToPersonality()
    {
        return new Personality
        {
            CharacterName = CharacterName,
            ClassName = ClassName,
            Level = Level,
            Background = Background,
            PlayerName = PlayerName,
            Race = Race,
            Attitute = Attitute,
            Experience = Experience,
        };
    }
}
