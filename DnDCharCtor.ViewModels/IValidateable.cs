using System.ComponentModel.DataAnnotations;

namespace DnDCharCtor.ViewModels;

public interface IValidateable
{
    bool HasValidationErrors { get; set; }

    Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; }

    string ValidationErrorSource { get; }

    bool Validate();
}
