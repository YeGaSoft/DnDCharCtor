namespace DnDCharCtor.Common.Services;

public interface IPlatformService
{
    ApplicationType GetApplicationType();
    Platform GetPlatform();

    Task<T?> GetFromStorageAsync<T>(string key);
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
