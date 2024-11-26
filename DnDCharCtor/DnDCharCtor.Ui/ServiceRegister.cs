using DnDCharCtor.Common.Constants;
using DnDCharCtor.Common.Resources;
using DnDCharCtor.Common.Services;
using DnDCharCtor.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDCharCtor.Ui;

public static class ServiceRegister
{
    public static IServiceCollection RegisterAll(this IServiceCollection services)
    {
        return services
            .RegisterServices()
            .RegisterViewModels()
            .RegisterUI()
            .AddLocalization();
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IJsonSerializerService, JsonSerializerService>();
        services.AddSingleton<ILocalizationService, LocalizationService>();
        services.AddSingleton<IHybridCacheService, HybridCacheService>();

        return services;
    }

    public static IServiceCollection RegisterViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<EditCharacterViewModel>();
        services.AddSingleton<SettingsViewModel>();

        return services;
    }

    public static IServiceCollection RegisterUI(this IServiceCollection services)
    {
        services.AddFluentUIComponents();
        services.AddSingleton<IDialogService, DialogService>();

        return services;
    }

    public static IServiceCollection AddLocalization(this IServiceCollection services)
    {
        services.AddLocalization(x => x.ResourcesPath = typeof(StringResources).Namespace!);
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var defaultCulture = new CultureInfo(Culture.EnglishCultureIdentifier);
            var supportedCultures = new[]
            {
                defaultCulture,
                new CultureInfo(Culture.GermanCultureIdentifier)
            };

            options.DefaultRequestCulture = new RequestCulture(defaultCulture);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        return services;
    }
}
