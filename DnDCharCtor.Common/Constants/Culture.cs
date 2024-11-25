using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Common.Constants;

public static class Culture
{
    public const string EnglishCultureIdentifier = "en-US";
    public const string GermanCultureIdentifier = "de-DE";

    public static bool IsLanguageSupported(string languageIdentifier)
    {
        return languageIdentifier switch
        {
            EnglishCultureIdentifier => true,
            GermanCultureIdentifier => true,
            _ => false,
        };
    }
}

