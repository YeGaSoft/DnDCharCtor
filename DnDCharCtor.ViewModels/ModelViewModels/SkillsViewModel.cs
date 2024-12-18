using CommunityToolkit.Mvvm.ComponentModel;
using DnDCharCtor.Models;
using DnDCharCtor.Resources;
using DnDCharCtor.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels.ModelViewModels;

public partial class SkillsViewModel : ObservableValidator, IViewModelBase<SkillsViewModel>
{
    public SkillsViewModel(Skills skills)
    {
        AcrobaticSkillfulness = skills.AcrobaticSkillfulness.ToString();
        ArcaneLoreIntelligence = skills.ArcaneLoreIntelligence.ToString();
        AthleticStrength = skills.AthleticStrength.ToString();
        AppearCharisma = skills.AppearCharisma.ToString();
        IntimidateCharisma = skills.IntimidateCharisma.ToString();
        DexteritySkillfulness = skills.DexteritySkillfulness.ToString();
        HistoryIntelligence = skills.HistoryIntelligence.ToString();
        MedicineWisdom = skills.MedicineWisdom.ToString();
        StealthSkillfulness = skills.StealthSkillfulness.ToString();
        AnimalHandlingWisdom = skills.AnimalHandlingWisdom.ToString();
        MotiveRecognitionWisdom = skills.MotiveRecognitionWisdom.ToString();
        ResearchIntelligence = skills.ResearchIntelligence.ToString();
        NaturalHistoryIntelligence = skills.NaturalHistoryIntelligence.ToString();
        ReligionIntelligence = skills.ReligionIntelligence.ToString();
        DeceiveCharisma = skills.DeceiveCharisma.ToString();
        SurvivalSkillWisdom = skills.SurvivalSkillWisdom.ToString();
        ConvinceCharisma = skills.ConvinceCharisma.ToString();
        PerceptionWisdom = skills.PerceptionWisdom.ToString();
    }

    public SkillsViewModel(SkillsViewModel viewModel)
    {
        AcrobaticSkillfulness = viewModel.AcrobaticSkillfulness;
        ArcaneLoreIntelligence = viewModel.ArcaneLoreIntelligence;
        AthleticStrength = viewModel.AthleticStrength;
        AppearCharisma = viewModel.AppearCharisma;
        IntimidateCharisma = viewModel.IntimidateCharisma;
        DexteritySkillfulness = viewModel.DexteritySkillfulness;
        HistoryIntelligence = viewModel.HistoryIntelligence;
        MedicineWisdom = viewModel.MedicineWisdom;
        StealthSkillfulness = viewModel.StealthSkillfulness;
        AnimalHandlingWisdom = viewModel.AnimalHandlingWisdom;
        MotiveRecognitionWisdom = viewModel.MotiveRecognitionWisdom;
        ResearchIntelligence = viewModel.ResearchIntelligence;
        NaturalHistoryIntelligence = viewModel.NaturalHistoryIntelligence;
        ReligionIntelligence = viewModel.ReligionIntelligence;
        DeceiveCharisma = viewModel.DeceiveCharisma;
        SurvivalSkillWisdom = viewModel.SurvivalSkillWisdom;
        ConvinceCharisma = viewModel.ConvinceCharisma;
        PerceptionWisdom = viewModel.PerceptionWisdom;
    }


    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_AcrobaticSkillfulness))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_AcrobaticSkillfulness), 1, int.MaxValue)]
    private string _acrobaticSkillfulness;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_ArcaneLoreIntelligence))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_ArcaneLoreIntelligence), 1, int.MaxValue)]
    private string _arcaneLoreIntelligence;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_AthleticStrength))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_AthleticStrength), 1, int.MaxValue)]
    private string _athleticStrength;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_AppearCharisma))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_AppearCharisma), 1, int.MaxValue)]
    private string _appearCharisma;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_IntimidateCharisma))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_IntimidateCharisma), 1, int.MaxValue)]
    private string _intimidateCharisma;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_DexteritySkillfulness))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_DexteritySkillfulness), 1, int.MaxValue)]
    private string _dexteritySkillfulness;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_HistoryIntelligence))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_HistoryIntelligence), 1, int.MaxValue)]
    private string _historyIntelligence;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_MedicineWisdom))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_MedicineWisdom), 1, int.MaxValue)]
    private string _medicineWisdom;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_StealthSkillfulness))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_StealthSkillfulness), 1, int.MaxValue)]
    private string _stealthSkillfulness;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_AnimalHandlingWisdom))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_AnimalHandlingWisdom), 1, int.MaxValue)]
    private string _animalHandlingWisdom;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_MotiveRecognitionWisdom))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_MotiveRecognitionWisdom), 1, int.MaxValue)]
    private string _motiveRecognitionWisdom;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_ResearchIntelligence))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_ResearchIntelligence), 1, int.MaxValue)]
    private string _researchIntelligence;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_NaturalHistoryIntelligence))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_NaturalHistoryIntelligence), 1, int.MaxValue)]
    private string _naturalHistoryIntelligence;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_ReligionIntelligence))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_ReligionIntelligence), 1, int.MaxValue)]
    private string _religionIntelligence;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_DeceiveCharisma))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_DeceiveCharisma), 1, int.MaxValue)]
    private string _deceiveCharisma;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_SurvivalSkillWisdom))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_SurvivalSkillWisdom), 1, int.MaxValue)]
    private string _survivalSkillWisdom;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_ConvinceCharisma))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_ConvinceCharisma), 1, int.MaxValue)]
    private string _convinceCharisma;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [LocalizedParsedIntegerRequired(nameof(StringResources.Character_Skills_PerceptionWisdom))]
    [LocalizedParsedRange(nameof(StringResources.Character_Skills_PerceptionWisdom), 1, int.MaxValue)]
    private string _perceptionWisdom;


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

    public SkillsViewModel CreateShallowCopy()
    {
        return new SkillsViewModel(this);
    }


    public bool Search(string searchText, bool includePropertyNames)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return true;
        }

        // Check string properties
        if (AcrobaticSkillfulness.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            ArcaneLoreIntelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            AthleticStrength.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            AppearCharisma.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            IntimidateCharisma.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            DexteritySkillfulness.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            HistoryIntelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            MedicineWisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StealthSkillfulness.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            AnimalHandlingWisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            MotiveRecognitionWisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            ResearchIntelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            NaturalHistoryIntelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            ReligionIntelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            DeceiveCharisma.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            SurvivalSkillWisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            ConvinceCharisma.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            PerceptionWisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (includePropertyNames is false) return false;

        // Check string resources
        if (StringResources.Character_Skills_AcrobaticSkillfulness.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_ArcaneLoreIntelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_AthleticStrength.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_AppearCharisma.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_IntimidateCharisma.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_DexteritySkillfulness.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_HistoryIntelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_MedicineWisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_StealthSkillfulness.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_AnimalHandlingWisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_MotiveRecognitionWisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_ResearchIntelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_NaturalHistoryIntelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_ReligionIntelligence.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_DeceiveCharisma.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_SurvivalSkillWisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_ConvinceCharisma.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            StringResources.Character_Skills_PerceptionWisdom.Contains(searchText, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }

    public Skills ToSkills()
    {
        var hasAcrobaticSkillfulness = int.TryParse(AcrobaticSkillfulness, out var acrobaticSkillfulness);
        var hasArcaneLoreIntelligence = int.TryParse(ArcaneLoreIntelligence, out var arcaneLoreIntelligence);
        var hasAthleticStrength = int.TryParse(AthleticStrength, out var athleticStrength);
        var hasAppearCharisma = int.TryParse(AppearCharisma, out var appearCharisma);
        var hasIntimidateCharisma = int.TryParse(IntimidateCharisma, out var intimidateCharisma);
        var hasDexteritySkillfulness = int.TryParse(DexteritySkillfulness, out var dexteritySkillfulness);
        var hasHistoryIntelligence = int.TryParse(HistoryIntelligence, out var historyIntelligence);
        var hasMedicineWisdom = int.TryParse(MedicineWisdom, out var medicineWisdom);
        var hasStealthSkillfulness = int.TryParse(StealthSkillfulness, out var stealthSkillfulness);
        var hasAnimalHandlingWisdom = int.TryParse(AnimalHandlingWisdom, out var animalHandlingWisdom);
        var hasMotiveRecognitionWisdom = int.TryParse(MotiveRecognitionWisdom, out var motiveRecognitionWisdom);
        var hasResearchIntelligence = int.TryParse(ResearchIntelligence, out var researchIntelligence);
        var hasNaturalHistoryIntelligence = int.TryParse(NaturalHistoryIntelligence, out var naturalHistoryIntelligence);
        var hasReligionIntelligence = int.TryParse(ReligionIntelligence, out var religionIntelligence);
        var hasDeceiveCharisma = int.TryParse(DeceiveCharisma, out var deceiveCharisma);
        var hasSurvivalSkillWisdom = int.TryParse(SurvivalSkillWisdom, out var survivalSkillWisdom);
        var hasConvinceCharisma = int.TryParse(ConvinceCharisma, out var convinceCharisma);
        var hasPerceptionWisdom = int.TryParse(PerceptionWisdom, out var perceptionWisdom);

        return new Skills
        {
            AcrobaticSkillfulness = hasAcrobaticSkillfulness ? acrobaticSkillfulness : 0,
            ArcaneLoreIntelligence = hasArcaneLoreIntelligence ? arcaneLoreIntelligence : 0,
            AthleticStrength = hasAthleticStrength ? athleticStrength : 0,
            AppearCharisma = hasAppearCharisma ? appearCharisma : 0,
            IntimidateCharisma = hasIntimidateCharisma ? intimidateCharisma : 0,
            DexteritySkillfulness = hasDexteritySkillfulness ? dexteritySkillfulness : 0,
            HistoryIntelligence = hasHistoryIntelligence ? historyIntelligence : 0,
            MedicineWisdom = hasMedicineWisdom ? medicineWisdom : 0,
            StealthSkillfulness = hasStealthSkillfulness ? stealthSkillfulness : 0,
            AnimalHandlingWisdom = hasAnimalHandlingWisdom ? animalHandlingWisdom : 0,
            MotiveRecognitionWisdom = hasMotiveRecognitionWisdom ? motiveRecognitionWisdom : 0,
            ResearchIntelligence = hasResearchIntelligence ? researchIntelligence : 0,
            NaturalHistoryIntelligence = hasNaturalHistoryIntelligence ? naturalHistoryIntelligence : 0,
            ReligionIntelligence = hasReligionIntelligence ? religionIntelligence : 0,
            DeceiveCharisma = hasDeceiveCharisma ? deceiveCharisma : 0,
            SurvivalSkillWisdom = hasSurvivalSkillWisdom ? survivalSkillWisdom : 0,
            ConvinceCharisma = hasConvinceCharisma ? convinceCharisma : 0,
            PerceptionWisdom = hasPerceptionWisdom ? perceptionWisdom : 0,
        };
    }

}

