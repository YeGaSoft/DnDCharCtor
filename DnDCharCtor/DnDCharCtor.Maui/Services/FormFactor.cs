using DnDCharCtor.Maui.Services;
using DnDCharCtor.Shared.Services;
using Microsoft.Maui.Devices;

namespace DnDCharCtor.Maui.Services
{
    public class FormFactor : IFormFactor
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
