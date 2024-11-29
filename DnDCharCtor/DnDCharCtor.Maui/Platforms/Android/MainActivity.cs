using Android.App;
using Android.Content.PM;
using Android.OS;
using DnDCharCtor.Maui.Platforms.Android;

namespace DnDCharCtor.Maui
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        // https://github.com/dotnet/maui/issues/14197#issuecomment-1535561632
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            WebViewSoftInputPatch.Initialize();
        }
    }
}
