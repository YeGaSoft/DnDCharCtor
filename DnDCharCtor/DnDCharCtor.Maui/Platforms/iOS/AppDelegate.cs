using Foundation;

namespace DnDCharCtor.Maui
{
    [Register("AppDelegate")]
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix // This file name comes from MAUI
    public class AppDelegate : MauiUIApplicationDelegate
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
