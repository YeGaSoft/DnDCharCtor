using DnDCharCtor.Common.Services;
using DnDCharCtor.Pwa.Constants;
using DnDCharCtor.Pwa.Services;
using Microsoft.JSInterop;

namespace DnDCharCtor.Pwa.Services;

public class PwaPlatformService : IPlatformService
{
    private readonly IJsonSerializerService _serializer;
    private readonly IJSRuntime _jsRuntime;

    public PwaPlatformService(IJsonSerializerService serializer, IJSRuntime jsRuntime)
    {
        _serializer = serializer;
        _jsRuntime = jsRuntime;
    }


    public ApplicationType GetApplicationType()
    {
        return ApplicationType.Pwa;
    }

    public Platform GetPlatform()
    {
        return Environment.OSVersion.Platform switch
        {
            PlatformID.Win32NT => Platform.Desktop,
            PlatformID.MacOSX => Platform.Desktop,
            PlatformID.Unix =>
                // You might need additional logic to differentiate between Android and iOS
                // For example, checking for specific environment variables or file paths
                Platform.Mobile,
            PlatformID.Other => Platform.Other,
            _ => Platform.Mobile,
        };
    }


    public async Task<T?> GetFromStorageAsync<T>(string key)
    {
        var value = await _jsRuntime.InvokeAsync<string?>(JsMethodNames.LocalStorageGetItem, key);
        if (value is null) return default;
        return _serializer.Deserialize<T>(value);
    }

    public async Task<bool> SetInStorageAsync<T>(string key, T value)
    {
        var json = _serializer.Serialize(value);
        if (string.IsNullOrWhiteSpace(json)) return false;

        await _jsRuntime.InvokeVoidAsync(JsMethodNames.LocalStorageSetItem, key, json);
        return true;
    }
}
