using Android.Widget;
using AndroidX.Activity;
using AndroidX.AppCompat.App;
using System;

namespace com.companyname.predictivebackgesturenet7
{
    public class BackPressedCallback : OnBackPressedCallback
    {
        private readonly AppCompatActivity activity;
        private long backPressedTime;

        public BackPressedCallback(AppCompatActivity activity) : base(true)
        {
            this.activity = activity;
        }

        public override void HandleOnBackPressed()
        {
            const int delay = 2000;
            if (backPressedTime + delay > DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
                activity.FinishAndRemoveTask();
            else
            {
                Toast.MakeText(activity, Resource.String.tap_back_again, ToastLength.Short)?.Show();
                backPressedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }
        }

    }

   
}
