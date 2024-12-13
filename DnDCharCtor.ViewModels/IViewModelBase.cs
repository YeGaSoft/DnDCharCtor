using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.ViewModels;

/// <summary>
/// This interface is only a wrapper for <see cref="IValidateable"/>, <see cref="IShallowCopyable{T}"/> and <see cref="ISearchable"/>.
/// It is intended for generic components that require these functionalities.
/// </summary>
/// <typeparam name="TViewModel">Type for <see cref="IShallowCopyable{T}"/> to create copies.</typeparam>
public interface IViewModelBase<TViewModel> : IValidateable, IShallowCopyable<TViewModel>, ISearchable, INotifyPropertyChanged
{
}
