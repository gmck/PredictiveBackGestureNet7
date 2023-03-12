using Android.App;
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
        
        public void OnActivityDestroyed(Activity activity)
        {
            if (!activity.IsChangingConfigurations)
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
