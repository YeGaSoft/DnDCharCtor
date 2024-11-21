using DnDCharCtor.Pwa.Services;
using DnDCharCtor.Ui.Services;

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
