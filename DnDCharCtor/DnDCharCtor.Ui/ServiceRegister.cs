using DnDCharCtor.Common.Constants;
using DnDCharCtor.Resources;
using DnDCharCtor.Common.Services;
using DnDCharCtor.ViewModels;
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
            .RegisterUI();
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IJsonSerializerService, JsonSerializerService>();
        services.AddSingleton<ILocalizationService, LocalizationService>();
        services.AddSingleton<IHybridCacheService, HybridCacheService>();
        services.AddSingleton<IEventAggregator, EventAggregator>();

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
        services.AddSingleton<Microsoft.FluentUI.AspNetCore.Components.IDialogService, DialogService>();

        return services;
    }
}
