using Android.Content;
using Android.Content.Res;

namespace com.companyname.predictivebackgesturenet7
{
    internal class NavigationBarUtils
    {
        // Constants defining the types of navigation available
        public const int NavigationBarThreeButtonMode = 0;
        public const int NavigationBarTwoButtonMode = 1;
        public const int NavigationBarGestureMode = 2;

        public static int GetNavigationBarInteractionMode(Context? context)
        {
            // What is the Mode in use?
            
            // According to ChatGPT config_navBarInteractionMode was added in 8.1 - it warns needs to be checked in future versions of Android.
            Resources resources = context!.Resources!;
            int resourceId = resources.GetIdentifier("config_navBarInteractionMode", "integer", "android");
            return resourceId > 0 ? resources.GetInteger(resourceId) : NavigationBarThreeButtonMode;
        }
    }
}
