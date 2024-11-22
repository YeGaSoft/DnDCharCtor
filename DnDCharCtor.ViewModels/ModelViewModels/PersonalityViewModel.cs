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
        Level = personality.Level.ToString();
        Background = personality.Background;
        PlayerName = personality.PlayerName;
        Race = personality.Race;
        Attitute = personality.Attitute;
        Experience = personality.Experience.ToString();
    }

    public PersonalityViewModel(PersonalityViewModel personality)
    {
        CharacterName = personality.CharacterName;
        ClassName = personality.ClassName;
        Level = personality.Level.ToString();
        Background = personality.Background;
        PlayerName = personality.PlayerName;
        Race = personality.Race;
        Attitute = personality.Attitute;
        Experience = personality.Experience.ToString();
    }

    [Required]
    [MaxLength(64)]
    public string CharacterName { get; set; }

    [Required]
    [MaxLength(32)]
    public string ClassName { get; set; }

    [Required]
    //[Range(1, int.MaxValue)]
    public string Level { get; set; }

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
    //[Range(0, int.MaxValue)]
    public string Experience { get; set; }

    public Personality ToPersonality()
    {
        return new Personality
        {
            CharacterName = CharacterName,
            ClassName = ClassName,
            Level = int.Parse(Level),
            Background = Background,
            PlayerName = PlayerName,
            Race = Race,
            Attitute = Attitute,
            Experience = int.Parse(Experience),
        };
    }
}
