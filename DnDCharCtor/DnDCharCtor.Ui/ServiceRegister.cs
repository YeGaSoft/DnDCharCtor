using DnDCharCtor.Common.Services;
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
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHybridCacheService, HybridCacheService>();
        services.AddSingleton<IJsonSerializer, JsonSerializer>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddFluentUIComponents();

        return services;
    }
}
