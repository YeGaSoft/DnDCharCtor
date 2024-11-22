using DnDCharCtor.Common.Services;
using DnDCharCtor.Maui.Services;
using DnDCharCtor.Ui;
using DnDCharCtor.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Fast.Components.FluentUI;
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

            builder.Services.AddSingleton<IPlatformService, MauiPlatformService>();
            builder.Services.RegisterServices();


            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
