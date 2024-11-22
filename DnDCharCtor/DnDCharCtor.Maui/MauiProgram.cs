using DnDCharCtor.Common.Services;
using DnDCharCtor.Maui.Services;
using DnDCharCtor.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace DnDCharCtor.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the DnDCharCtor.Maui.Shared project
            builder.Services.AddSingleton<IPlatformService, MauiPlatformService>();
            builder.Services.AddSingleton<IHybridCacheService, HybridCacheService>();
            builder.Services.AddSingleton<IJsonSerializer, JsonSerializer>();
            builder.Services.AddSingleton<MainViewModel>();


            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
