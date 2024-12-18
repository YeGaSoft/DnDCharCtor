using DnDCharCtor.Resources;
using DnDCharCtor.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Models;

public record Skills
{
    [LocalizedRequired(nameof(StringResources.Character_Skills_AcrobaticSkillfulness))]
    [LocalizedRange(nameof(StringResources.Character_Skills_AcrobaticSkillfulness), 1, int.MaxValue)]
    public required int AcrobaticSkillfulness { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_ArcanLoreIntelligence))]
    [LocalizedRange(nameof(StringResources.Character_Skills_ArcanLoreIntelligence), 1, int.MaxValue)]
    public required int ArcanLoreIntelligence { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_AthleticStrength))]
    [LocalizedRange(nameof(StringResources.Character_Skills_AthleticStrength), 1, int.MaxValue)]
    public required int AthleticStrength { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_AppearCharisma))]
    [LocalizedRange(nameof(StringResources.Character_Skills_AppearCharisma), 1, int.MaxValue)]
    public required int AppearCharisma { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_IntimidateCharisma))]
    [LocalizedRange(nameof(StringResources.Character_Skills_IntimidateCharisma), 1, int.MaxValue)]
    public required int IntimidateCharisma { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_DexteritySkillfulness))]
    [LocalizedRange(nameof(StringResources.Character_Skills_DexteritySkillfulness), 1, int.MaxValue)]
    public required int DexteritySkillfulness { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_HistoryIntelligence))]
    [LocalizedRange(nameof(StringResources.Character_Skills_HistoryIntelligence), 1, int.MaxValue)]
    public required int HistoryIntelligence { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_MedicineWisdom))]
    [LocalizedRange(nameof(StringResources.Character_Skills_MedicineWisdom), 1, int.MaxValue)]
    public required int MedicineWisdom { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_StealthSkillfulness))]
    [LocalizedRange(nameof(StringResources.Character_Skills_StealthSkillfulness), 1, int.MaxValue)]
    public required int StealthSkillfulness { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_AnimalHandlingWisdom))]
    [LocalizedRange(nameof(StringResources.Character_Skills_AnimalHandlingWisdom), 1, int.MaxValue)]
    public required int AnimalHandlingWisdom { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_MotiveRecognitionWisdom))]
    [LocalizedRange(nameof(StringResources.Character_Skills_MotiveRecognitionWisdom), 1, int.MaxValue)]
    public required int MotiveRecognitionWisdom { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_ResearchIntelligence))]
    [LocalizedRange(nameof(StringResources.Character_Skills_ResearchIntelligence), 1, int.MaxValue)]
    public required int ResearchIntelligence { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_NaturalHistoryIntelligence))]
    [LocalizedRange(nameof(StringResources.Character_Skills_NaturalHistoryIntelligence), 1, int.MaxValue)]
    public required int NaturalHistoryIntelligence { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_ReligionIntelligence))]
    [LocalizedRange(nameof(StringResources.Character_Skills_ReligionIntelligence), 1, int.MaxValue)]
    public required int ReligionIntelligence { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_DeceiveCharisma))]
    [LocalizedRange(nameof(StringResources.Character_Skills_DeceiveCharisma), 1, int.MaxValue)]
    public required int DeceiveCharisma { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_SurvivalSkillWisdom))]
    [LocalizedRange(nameof(StringResources.Character_Skills_SurvivalSkillWisdom), 1, int.MaxValue)]
    public required int SurvivalSkillWisdom { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_ConvinceCharisma))]
    [LocalizedRange(nameof(StringResources.Character_Skills_ConvinceCharisma), 1, int.MaxValue)]
    public required int ConvinceCharisma { get; init; }

    [LocalizedRequired(nameof(StringResources.Character_Skills_PerceptionWisdom))]
    [LocalizedRange(nameof(StringResources.Character_Skills_PerceptionWisdom), 1, int.MaxValue)]
    public required int PerceptionWisdom { get; init; }

    public static Skills Empty => new()
    {
        AcrobaticSkillfulness = 0,
        ArcanLoreIntelligence = 0,
        AthleticStrength = 0,
        AppearCharisma = 0,
        IntimidateCharisma = 0,
        DexteritySkillfulness = 0,
        HistoryIntelligence = 0,
        MedicineWisdom = 0,
        StealthSkillfulness = 0,
        AnimalHandlingWisdom = 0,
        MotiveRecognitionWisdom = 0,
        ResearchIntelligence = 0,
        NaturalHistoryIntelligence = 0,
        ReligionIntelligence = 0,
        DeceiveCharisma = 0,
        SurvivalSkillWisdom = 0,
        ConvinceCharisma = 0,
        PerceptionWisdom = 0,
    };
}
