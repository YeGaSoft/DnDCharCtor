using System.Runtime.InteropServices.JavaScript;

namespace DnDCharCtor.Pwa;

// https://github.com/dotnet/aspnetcore/issues/50361#issuecomment-1699512477
// https://github.com/dotnet/runtime/issues/98415#issuecomment-1945859860
internal partial class WebAssemblyCultureProviderInterop
{
    // Unfortunally, JSImport requires "AllowUnsafeBlocks" in csproj
    [JSImport("INTERNAL.loadSatelliteAssemblies")]
    public static partial Task LoadSatelliteAssemblies(string[] culturesToLoad);
}
