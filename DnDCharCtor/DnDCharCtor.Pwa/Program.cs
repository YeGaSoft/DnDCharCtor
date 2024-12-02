using DnDCharCtor.Pwa;
using DnDCharCtor.Pwa.Services;
using DnDCharCtor.Ui;
using DnDCharCtor.Common.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DnDCharCtor.Common.Constants;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IPlatformService, PwaPlatformService>();
builder.Services.RegisterAll();

// https://github.com/dotnet/aspnetcore/issues/50361#issuecomment-1699512477
await WebAssemblyCultureProviderInterop.LoadSatelliteAssemblies(new string[] { Culture.EnglishCultureIdentifier, Culture.GermanCultureIdentifier, });

await builder.Build().RunAsync();