namespace DnDCharCtor.Common.Services;

public interface IPlatformService
{
    ApplicationType GetApplicationType();
    Platform GetPlatform();

    Task<string> GetSystemLanguageIdentifierAsync();

    Task<T?> GetFromStorageAsync<T>(string key);
    Task<bool> SetInStorageAsync<T>(string key, T value);
    Task<bool> RemoveFromStorageAsync(string key);
}

public enum ApplicationType
{
    Maui,
    Pwa,
}

public enum Platform
{
    Desktop,
    Mobile,
    Other,
}
