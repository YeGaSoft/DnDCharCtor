using DnDCharCtor.Pwa.Services;
using DnDCharCtor.Shared.Services;

namespace DnDCharCtor.Pwa.Services
{
    public class FormFactor : IFormFactor
    {
        public string GetFormFactor()
        {
            return "WebAssembly";
        }

        public string GetPlatform()
        {
            return Environment.OSVersion.ToString();
        }
    }
}
