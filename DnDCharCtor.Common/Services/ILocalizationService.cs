using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Common.Services;

public interface ILocalizationService : INotifyPropertyChanged
{
    bool IsLanguageSupported(string languageIdentifier);

    void ChangeCulture(CultureInfo cultureInfo);
}
