using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels;

public interface IValidateableViewModel : INotifyPropertyChanged
{
    bool HasValidationErrors { get; set; }

    Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; }

    string ValidationErrorSource { get; }

    bool Validate();
}
