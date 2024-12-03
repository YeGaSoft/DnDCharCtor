using DnDCharCtor.Common.Services;
using DnDCharCtor.Maui.Services;
using Microsoft.Maui.Devices;
using System.Globalization;
using System.Text.Json;

namespace DnDCharCtor.Maui.Services;

internal class MauiPlatformService : IPlatformService
{
    private readonly IDatabaseService _databaseService;

    public MauiPlatformService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public ApplicationType GetApplicationType()
    {
        return ApplicationType.Maui;
    }

    public Common.Services.Platform GetPlatform()
    {
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            return Common.Services.Platform.Mobile;
        }
        else if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            return Common.Services.Platform.Mobile;
        }
        else if (DeviceInfo.Platform == DevicePlatform.macOS)
        {
            return Common.Services.Platform.Desktop;
        }
        else if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            return Common.Services.Platform.Desktop;
        }
        else
        {
            return Common.Services.Platform.Other;
        }
    }

    public Task<string> GetSystemLanguageIdentifierAsync()
    {
        return Task.FromResult(CultureInfo.CurrentCulture.Name);
    }


    public async Task<T?> GetFromStorageAsync<T>(string key)
    {
        return await _databaseService.GetItemAsync<T>(key);
    }

    public async Task<bool> SetInStorageAsync<T>(string key, T value)
    {
        var changesCount = await _databaseService.SaveItemAsync(key, value);
        return changesCount > 0;
    }
}
