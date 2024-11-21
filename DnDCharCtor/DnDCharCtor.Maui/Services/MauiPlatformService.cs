using DnDCharCtor.Common.Services;
using DnDCharCtor.Maui.Services;
using Microsoft.Maui.Devices;
using System.Text.Json;

namespace DnDCharCtor.Maui.Services;

public class MauiPlatformService : IPlatformService
{
    private IJsonSerializer _serializer;

    public MauiPlatformService(IJsonSerializer serializer)
    {
        _serializer = serializer;
    }

    public ApplicationType GetApplicationType()
    {
        return ApplicationType.Maui;
    }

    public async Task<T?> GetFromStorageAsync<T>(string key)
    {
        var value = await SecureStorage.Default.GetAsync(key);
        if (value is null) return default;
        return _serializer.Deserialize<T>(value);
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
}
