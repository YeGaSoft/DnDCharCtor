using DnDCharCtor.Maui.Services;
using DnDCharCtor.Ui.Services;
using Microsoft.Maui.Devices;

namespace DnDCharCtor.Maui.Services
{
    public class PlatformService : IPlatformService
    {
        public string GetFormFactor()
        {
            return DeviceInfo.Idiom.ToString();
        }

        public string GetPlatform()
        {
            return DeviceInfo.Platform.ToString() + " - " + DeviceInfo.VersionString;
        }
    }
}
