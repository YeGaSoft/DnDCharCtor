using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Resources;
using DnDCharCtor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DnDCharCtor.Common.Services;

namespace DnDCharCtor.ViewModels.ModelViewModels;

public partial class CharacterViewModel : ObservableValidator, IViewModelBase<CharacterViewModel>
{
    private readonly IEquipmentService _equipmentService;
    private readonly IStatsService _statsService;
    private readonly StatsViewModel _statsViewModel;

    public CharacterViewModel(Character character)
    {
        _equipmentService = new EquipmentService();
        _statsService = new StatsService(_equipmentService);
        _statsViewModel = new(this, _statsService);

        CharacterId = character.Id;
        PersonalityViewModel = new(character.Personality);
        PropertiesViewModel = new(character.Properties, _statsViewModel);
        RescueDicesViewModel = new(character.RescueDices);
        SkillsViewModel = new SkillsViewModel(character.Skills);
    }

    public CharacterViewModel(CharacterViewModel characterViewModel)
    {
        _equipmentService = new EquipmentService();
        _statsService = new StatsService(_equipmentService);
        _statsViewModel = new(this, _statsService);

        CharacterId = characterViewModel.CharacterId;
        PersonalityViewModel = new(characterViewModel.PersonalityViewModel);
        PropertiesViewModel = new(characterViewModel.PropertiesViewModel, _statsViewModel);
        RescueDicesViewModel = new(characterViewModel.RescueDicesViewModel);
        SkillsViewModel = new(characterViewModel.SkillsViewModel);
    }

    public Guid CharacterId { get; set; }

    [ObservableProperty]
    private PersonalityViewModel _personalityViewModel;

    [ObservableProperty]
    private PropertiesViewModel _propertiesViewModel;

    [ObservableProperty]
    private RescueDicesViewModel _rescueDicesViewModel;

    [ObservableProperty]
    private SkillsViewModel _skillsViewModel;

    [ObservableProperty]
    private bool _hasValidationErrors;
    public Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; } = [];
    public string ValidationErrorSource => StringResources.Character_Name;


    public bool Validate()
    {
        bool personalityValid = PersonalityViewModel.Validate();
        bool propertiesValid = PropertiesViewModel.Validate();
        bool rescueDicesValid = RescueDicesViewModel.Validate();
        bool skillsValid = SkillsViewModel.Validate();

        HasValidationErrors = personalityValid && propertiesValid && rescueDicesValid;
        ValidationErrors.Clear();
        ValidationErrors = 
            ValidationErrors
            .Concat(PersonalityViewModel.ValidationErrors)
            .Concat(PropertiesViewModel.ValidationErrors)
            .Concat(RescueDicesViewModel.ValidationErrors)
            .Concat(SkillsViewModel.ValidationErrors)
            .Where(validationErrorSource => validationErrorSource.Value.Count > 0)
            .ToDictionary();
        return HasValidationErrors is false;
    }

    public CharacterViewModel CreateShallowCopy()
    {
        return new CharacterViewModel(this);
    }

    public bool Search(string searchText, bool includePropertyNames)
    {
        return PersonalityViewModel.Search(searchText, includePropertyNames) ||
            PropertiesViewModel.Search(searchText, includePropertyNames) ||
            RescueDicesViewModel.Search(searchText, includePropertyNames) ||
            SkillsViewModel.Search(searchText, includePropertyNames);
    }


    public Character ToCharacter()
    {
        return new Character
        {
            Id = CharacterId,
            Personality = PersonalityViewModel.ToPersonality(),
            Properties = PropertiesViewModel.ToProperties(),
            RescueDices = RescueDicesViewModel.ToRescueDices(),
            Skills = SkillsViewModel.ToSkills(),
        };
    }
}
