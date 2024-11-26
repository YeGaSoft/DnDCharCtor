Blazor.start({
    webAssembly: {
        configureRuntime: dotnet => {
            dotnet.withConfig({ loadAllSatelliteResources: true });
        }
    }
});