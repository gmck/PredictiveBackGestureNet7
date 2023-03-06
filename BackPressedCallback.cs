using Android.Widget;
using AndroidX.Activity;
using AndroidX.AppCompat.App;

namespace com.companyname.predictivebackgesturenet7
{
    public class BackPressedCallback : OnBackPressedCallback
    {
        private readonly AppCompatActivity activity;
        
        public BackPressedCallback(AppCompatActivity activity) : base(true)
        {
            this.activity = activity;
        }

        public override void HandleOnBackPressed()
        {
            // Determine the type of navigation, so that we can adjust the message of the Toast
            int navigationMode = NavigationBarUtils.GetNavigationBarInteractionMode(activity);
            string message = navigationMode != NavigationBarUtils.NavigationBarGestureMode ? "Tap the back button again to exit the app" : "Swipe again to exit the app";
            Toast.MakeText(activity, message, ToastLength.Short)?.Show();
            Remove();
        }
    }

   
}
