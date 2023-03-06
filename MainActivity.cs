using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using AndroidX.AppCompat.App;
using System;

namespace com.companyname.predictivebackgesturenet7
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]  // Theme = "@style/Theme.PredictiveBackGesture" - Not required - see Styles.xml and postSplashScreenTheme
    public class MainActivity : AppCompatActivity
    {
        private readonly string logTag = "PredictiveBackgesture";
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            AndroidX.Core.SplashScreen.SplashScreen.InstallSplashScreen(this);
            //WindowCompat.SetDecorFitsSystemWindows(Window!, false);
            
            base.OnCreate(savedInstanceState);
            
            if (OperatingSystem.IsAndroidVersionAtLeast(28))
                Window!.Attributes!.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.Default;

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // Add a OnBackPressedCallback if required
            if (OperatingSystem.IsAndroidVersionAtLeast(33) && NavigationBarUtils.GetNavigationBarInteractionMode(this) != NavigationBarUtils.NavigationBarGestureMode)
                OnBackPressedDispatcher.AddCallback(this, new BackPressedCallback(this));
            else if (!OperatingSystem.IsAndroidVersionAtLeast(33))
                OnBackPressedDispatcher.AddCallback(this, new BackPressedCallback(this));
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