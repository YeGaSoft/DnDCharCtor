using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Common.Events;

/// <summary>
/// This event is fired when data from the current character changed or when a new current character was set/created.
/// This usally should cause to also trigger <see cref="CharactersChangedEvent"/>.
/// </summary>
public class CurrentCharacterChangedEvent : PubSubEvent
{
}
