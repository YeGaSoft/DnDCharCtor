using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels;

/// <summary>
/// This interface is only a wrapper for <see cref="IValidateableViewModel"/> and <see cref="IShallowCopyable{T}"/>.
/// It is intended for generic components that require these functionalities.
/// </summary>
/// <typeparam name="TViewModel">Type for <see cref="IShallowCopyable{T}"/> to create copies.</typeparam>
public interface IValidateAndCopyableViewModel<TViewModel> : IValidateableViewModel, IShallowCopyable<TViewModel>
{
}

public interface IValidateableViewModel : INotifyPropertyChanged
{
    bool HasValidationErrors { get; set; }

    Dictionary<string, IReadOnlyCollection<ValidationResult>> ValidationErrors { get; set; }

    string ValidationErrorSource { get; }

    bool Validate();
}
