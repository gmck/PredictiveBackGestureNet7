using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using System;

namespace com.companyname.predictivebackgesturenet7
{
    [Application]
    public class PredictiveBackGestureApplication : Application, Application.IActivityLifecycleCallbacks
    {
        protected PredictiveBackGestureApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        public PredictiveBackGestureApplication() { }
        
        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
        }
        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        // This fires before OnActivityDestroyed
        //public override void OnTrimMemory([GeneratedEnum] TrimMemory level)
        //{
        //    base.OnTrimMemory(level);
        //    if (level == TrimMemory.UiHidden)
        //    {
        //        // stop your service here
        //        StopService(new Intent(this, typeof(MyService)));
        //        // ...
        //    }
        //}

        public void OnActivityDestroyed(Activity activity)
        {
            //if (!activity.IsChangingConfigurations )
            //    activity.Finish();

            if (activity is MainActivity && !activity.IsFinishing)
                activity.Finish();
            
        }
        public void OnActivityCreated(Activity activity, Bundle? savedInstanceState) { }
        public void OnActivityPaused(Activity activity) { }
        public void OnActivityResumed(Activity activity) { }
        public void OnActivitySaveInstanceState(Activity activity, Bundle? outState) { }
        public void OnActivityStarted(Activity activity) { }
        public void OnActivityStopped(Activity activity) { }
    }
}
