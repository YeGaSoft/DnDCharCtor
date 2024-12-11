using Android.Content.Res;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using System.Runtime.Versioning;
using static Android.Resource;
using Activity = Android.App.Activity;
using Rect = Android.Graphics.Rect;
using View = Android.Views.View;

namespace DnDCharCtor.Maui.Platforms.Android;

// https://github.com/dotnet/maui/issues/14197#issuecomment-1535561632
[SupportedOSPlatform("Android")]
public static class WebViewSoftInputPatch
{
    private static Activity Activity => Platform.CurrentActivity ?? throw new InvalidOperationException("Android Activity can't be null.");
    private static View MChildOfContent = default!;
    private static FrameLayout.LayoutParams FrameLayoutParams = default!;
    private static int UsableHeightPrevious = 0;

    // The Initialize method sets up the patch by finding the main content view of the current activity and attaching a global layout listener to it.
    // It ensures that the necessary views and layout parameters are not null.
    public static void Initialize()
    {
        var content = (FrameLayout?)Activity.FindViewById(Id.Content);
        ArgumentNullException.ThrowIfNull(content, nameof(content));
        var childContent = content.GetChildAt(0);
        ArgumentNullException.ThrowIfNull(childContent, nameof(childContent));
        MChildOfContent = childContent;
        ArgumentNullException.ThrowIfNull(MChildOfContent.ViewTreeObserver, nameof(MChildOfContent.ViewTreeObserver));
        MChildOfContent.ViewTreeObserver.GlobalLayout += (s, o) => PossiblyResizeChildOfContent();
        var frameLayoutParams = (FrameLayout.LayoutParams?)MChildOfContent?.LayoutParameters;
        ArgumentNullException.ThrowIfNull(frameLayoutParams, nameof(frameLayoutParams));
        FrameLayoutParams = frameLayoutParams;
    }

    // The PossiblyResizeChildOfContent method is called whenever the global layout changes.
    // It calculates the usable height of the content view and adjusts its height based on whether the keyboard is visible or not.
    // If the height difference (indicating the keyboard is visible) is significant, it resizes the content view to fit the remaining space.
    private static void PossiblyResizeChildOfContent()
    {
        var usableHeightNow = ComputeUsableHeight();
        if (usableHeightNow != UsableHeightPrevious)
        {
            var usableHeightSansKeyboard = MChildOfContent.RootView?.Height ?? 0;
            var heightDifference = usableHeightSansKeyboard - usableHeightNow;
            if (heightDifference < 0)
            {
                usableHeightSansKeyboard = MChildOfContent.RootView?.Width ?? 0;
                heightDifference = usableHeightSansKeyboard - usableHeightNow;
            }

            if (heightDifference > usableHeightSansKeyboard / 4)
            {
                FrameLayoutParams.Height = usableHeightSansKeyboard - heightDifference;
            }
            else
            {
                FrameLayoutParams.Height = usableHeightNow;
            }
        }

        MChildOfContent.RequestLayout();
        UsableHeightPrevious = usableHeightNow;
    }

    // The ComputeUsableHeight method calculates the usable height of the content view, taking into account the status bar height if the device is not in immersive mode.
    private static int ComputeUsableHeight()
    {
        var rect = new Rect();
        MChildOfContent.GetWindowVisibleDisplayFrame(rect);
        if (IsImmersiveMode())
        {
            return rect.Bottom;
        }
        else
        {
            return rect.Bottom - GetStatusBarHeight();
        }
    }

    // The GetStatusBarHeight method retrieves the height of the status bar from the system resources.
    private static int GetStatusBarHeight()
    {
        var result = 0;
        var resources = Activity.Resources;
        var resourceId = resources?.GetIdentifier("status_bar_height", "dimen", "android");
        if (resourceId is not null && resourceId > 0)
        {
            result = resources?.GetDimensionPixelSize(resourceId.Value) ?? 0;
        }
        return result;
    }

    // Originally from:
    // https://github.com/dotnet/maui/issues/14197#issuecomment-1535561632
    // But `SystemUiVisibility` is deprecated.
    /*
    private static bool IsImmersiveMode()
    {
        var decorView = Activity.Window?.DecorView;
        //var uiOptions = (int)decorView.SystemUiVisibility;
        var uiOptions = (int)decorView.SystemUiVisibility;
        return (uiOptions & (int)SystemUiFlags.Immersive) == (int)SystemUiFlags.Immersive;
    }
    */

    // New solution since `SystemUiVisibility` is deprecated.
    // https://learn.microsoft.com/en-us/dotnet/api/android.views.view.systemuivisibility?view=net-android-34.0
    // https://learn.microsoft.com/en-us/dotnet/api/android.views.windowmanagerlayoutparams.systemuivisibility?view=net-android-34.0
    //
    // The IsImmersiveMode method checks if the device is in immersive mode using the WindowCompat.GetInsetsController method,
    // which is the updated way to handle system UI visibility since SystemUiVisibility is deprecated.
    //
    // Immersive mode on Android is a feature that allows apps to take up the entire screen by hiding the system's navigation and status bars.
    // This provides a distraction-free experience, ideal for activities like watching videos, playing games, or reading.
    // Here's how it works:
    // 1. **Full-Screen Experience**:
    // - When immersive mode is enabled, the app content extends to cover the entire screen, hiding the navigation and status bars.
    // This maximizes the viewing area and minimizes interruptions
    // (https://www.xda-developers.com/how-to-enable-system-wide-immersive-mode-without-root/).
    // 2. **User Interaction**:
    // - Users can still access the hidden bars by swiping from the edges of the screen.
    // This temporary reveal allows users to navigate or check notifications without permanently exiting immersive mode
    // (https://citizenside.com/technology/how-to-enable-immersive-mode-on-android/).
    // 3. **Implementation**:
    // - Developers can enable immersive mode in their apps by using specific flags and methods provided by the Android API.
    // For example, they can use `WindowInsetsControllerCompat` to control the visibility of system bars
    // (https://developer.android.com/design/ui/mobile/guides/layout-and-content/immersive-content).
    // 4. **Benefits**:
    // - Immersive mode enhances the user experience by providing a more engaging and uninterrupted interaction with the app content.
    // It's particularly useful for media-centric applications
    // (https://citizenside.com/technology/how-to-enable-immersive-mode-on-android/).
    private static bool IsImmersiveMode()
    {
        var window = Activity.Window;
        if (window is null) return false;

        var decorView = window.DecorView;
        if (decorView is null) return false;

        var insetsController = WindowCompat.GetInsetsController(window, decorView);
        if (insetsController is null) return false;

        var isImmersive = (insetsController.SystemBarsBehavior & WindowInsetsControllerCompat.BehaviorShowTransientBarsBySwipe) != 0;
        return isImmersive;
    }
}
