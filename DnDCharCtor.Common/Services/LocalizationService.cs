using DnDCharCtor.Common.Constants;
using DnDCharCtor.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Common.Services;

public class LocalizationService : ILocalizationService
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsLanguageSupported(string languageIdentifier)
    {
        return languageIdentifier switch
        {
            Culture.EnglishCultureIdentifier => true,
            Culture.GermanCultureIdentifier => true,
            _ => false,
        };
    }

    public void ChangeCulture(CultureInfo cultureInfo)
    {
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        
        StringResources.Culture = cultureInfo;
        PropertyChanged?.Invoke(this, new(nameof(StringResources.Culture)));
    }
}
