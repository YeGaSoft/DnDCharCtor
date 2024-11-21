using DnDCharCtor.Pwa;
using DnDCharCtor.Pwa.Services;
using DnDCharCtor.Ui;
using DnDCharCtor.Common.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Routes>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IPlatformService, PwaPlatformService>();


await builder.Build().RunAsync();
