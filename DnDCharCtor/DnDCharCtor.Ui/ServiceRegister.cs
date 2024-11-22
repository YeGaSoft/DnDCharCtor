﻿using DnDCharCtor.Common.Services;
using DnDCharCtor.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Fast.Components.FluentUI;
using System;
using System.Collections.Generic;
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
        services.AddSingleton<IHybridCacheService, HybridCacheService>();

        return services;
    }

    public static IServiceCollection RegisterViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<EditCharacterViewModel>();

        return services;
    }

    public static IServiceCollection RegisterUI(this IServiceCollection services)
    {
        services.AddFluentUIComponents();
        services.AddSingleton<IDialogService, DialogService>();

        return services;
    }
}