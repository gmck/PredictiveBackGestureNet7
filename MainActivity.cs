using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using AndroidX.AppCompat.App;
using System;
using System.Diagnostics;

namespace com.companyname.predictivebackgesturenet7
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]  // Theme = "@style/Theme.PredictiveBackGesture" - Not required - see styles.xml and postSplashScreenTheme
    public class MainActivity : AppCompatActivity
    {
        private readonly string logTag = "PredictiveBackgesture";
        
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            AndroidX.Core.SplashScreen.SplashScreen.InstallSplashScreen(this);
            base.OnCreate(savedInstanceState);
           
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // Only add a callback on Android 13 devices if using 3-button navigation - everything else adds a callback
            // Another way of putting it Android 13 devices using gesture navigation don't get the callback - all other devices get it
            //if (OperatingSystem.IsAndroidVersionAtLeast(33) && NavigationBarUtils.GetNavigationBarInteractionMode(this) != NavigationBarUtils.NavigationBarGestureMode)
            //    OnBackPressedDispatcher.AddCallback(this, new BackPressedCallback(this));
            //else if (!OperatingSystem.IsAndroidVersionAtLeast(33))
            //    OnBackPressedDispatcher.AddCallback(this, new BackPressedCallback(this));


            // Mar 11 2023 - Only add a callback on Android 13 devices if using 3 - button navigation - everything else adds a callback
            // Devices less than Android 13 the same, but restricted to those users that are still using 3-button navigation.
            if (OperatingSystem.IsAndroidVersionAtLeast(33) && NavigationBarUtils.GetNavigationBarInteractionMode(this) != NavigationBarUtils.NavigationBarGestureMode)
                OnBackPressedDispatcher.AddCallback(this, new BackPressedCallback(this));
            else if (!OperatingSystem.IsAndroidVersionAtLeast(33) && NavigationBarUtils.GetNavigationBarInteractionMode(this) == NavigationBarUtils.NavigationBarThreeButtonMode)
                OnBackPressedDispatcher.AddCallback(this, new BackPressedCallback(this));


            if (OperatingSystem.IsAndroidVersionAtLeast(28))
                Window!.Attributes!.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.Default;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (IsFinishing)
                StopService();

            Log.Debug(logTag, "OnDestroy IsFinishing is " + IsFinishing.ToString());
        }

        public void StopService()
        {
            // Called from OnDestroy when IsFinishing is true
            Log.Debug(logTag, "StopService - called from OnDestroy");
        }
    }
}