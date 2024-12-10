using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Ui.Constants;

public static class Routes
{
    public const string Home = "/";

    public const string CurrentCharacter = "currentCharacter";
    public const string CreateCharacter = "createCharacter";
    public const string EditCharacter = "editCharacter";
    public const string EditCharacterQueryParameterForceNew = "forceNew";
    public const string EditCharacterQueryParameterId = "id";

    public const string Characters = "characters";

    public const string EditCharacterWithForceNew = $"{EditCharacter}?{EditCharacterQueryParameterForceNew}=True";

    public const string Settings = "settings";
    public const string Info = "info";
}
