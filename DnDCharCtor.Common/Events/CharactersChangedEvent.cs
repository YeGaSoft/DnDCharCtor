using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Common.Events;

/// <summary>
/// This event is fired when characters are added or deleted.
/// It also may be fired after <see cref="CurrentCharacterChangedEvent"/>.
/// </summary>
public class CharactersChangedEvent : PubSubEvent
{
}
