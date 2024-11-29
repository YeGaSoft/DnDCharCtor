// https://github.com/dotnet/runtime/issues/98415#issuecomment-2031216152
Blazor.start({
    webAssembly: {
        configureRuntime: dotnet => {
            dotnet.withConfig({ loadAllSatelliteResources: true });
        }
    }
});