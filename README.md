<p style="text-align: center;">PredictiveBackGestureNet7</p>

**Using Predictive Back Gesture in conjunction with an OnBackPressedCallback**

Since Android 10 we had swipe gestures for navigating in our apps and basically, most users have adapted and moved away from using three-button navigation. 

The most obvious advantage is more screen real estate to work with while also giving our apps a more immersive look. As an example on one screen in my app containing a RecyclerView and a BottomNavigationView, I can display 8.5 items in the RecyclerView without scrolling as compared to 10 items when there is only a sliver of a NavigationBar as you have when you remove the 3-button NavigationBar.

A while back I did a survey of my users regarding back button navigation compared to swipe navigation and was pleased to see that very few users of Android 10 devices and above were still using 3-button navigation. I’ll admit I was a relatively slow adopter of swiping for navigation, but if you have been here before then you will already know that I’ve adopted Google’s Navigation Component for all my apps and when using the Navigation Component you tend to just automatically adapt to the swipe navigation. These days when testing 3-button navigation, I still find myself automatically swiping to exit a fragment, forgetting that I should be tapping the back navigation button.

Of course, if we support devices lower than Android 10 we have to support 3-button navigation, however, you tend to think that after 4 years of swipe navigation, the days of 3-button navigation are numbered as Google raises the minimum SDK value with each new version of Android.

If you read this far, then you probably wondering what the above has got to do with the new Predictive Back Gesture which will be available in Android 14 and is available now in Android 13 assuming you have turned on Predictive Back Animations via Developer Options.  

When you first read up on Predictive Back Gesture the first thing that Google’s docs comment on is that you need to be using  OnBackPressedCallback which is available in Xamarin.AndroidX.AppCompat 1.6.0.1. Also available in version 1.5.1.1. However, that is not entirely true.

The real problem is Activity.OnBackPress has been deprecated. In other words, the OnBackPressedCallback replaces Activity.OnBackPressed. Therefore all the code you may have used in OnBackPressed now needs to be done in an OnBackPressedCallback.

However, on first reading the docs it sort of implies that the Predictive Back Gesture is tied to the OnBackPressedCallback when in fact if an Activity or alternatively a start destination fragment using the Navigation Component has an active OnBackPressedCallback, that callback will actually prevent the Predictive Back Gesture from happening and the app will close normally without the Predictive Back Gesture.

The question then obviously follows, why do we need an OnBackPressedCallback? The short answer is we don’t need one.  

However, without one we will not be able to supply the behaviour that we may have had for exiting the app when using 3-button navigation. A typical 3-button navigation may have used a “Do you want to exit Yes/No” dialog or a double back key tap to display a Toast “Do you want to exit?” on the first tap, with a confirming back key tap to exit the app within a small time period. 

I used to use the double-back key tap technique in my apps prior to Android 10 but dropped that technique after changing to gesture navigation from Android 10 onwards. I would argue if an app starts on the first fragment displayed and exits on the same fragment then I doubt that a user is going to have premature exits more than once or twice when using the app for the first time. All the Predictive Back Gesture will do is probably reduce that to one false exit, while the user comes to terms with exactly what the Predictive Back Gesture does.

By default, any net7.0-android or xamarin.android app using the blank app template targeting Android 13 will default to displaying a Predictive Back Gesture on exiting under the following conditions. 

1.	The device is running Android 13.
2.	The androidmanifest.xml file contains the following entry in the application tag android:enableOnBackInvokedCallback="true". 
3.	Predictive Back Animations is enabled in Developer Settings.
4.	The device is set to use Gesture navigation – not 3-button navigation.

I don’t want to give the impression that I don’t use OnBackPressedCallbacks for I do use them in all the fragments that are part of the NavigationGraph series of tutorials here. The OnBackPressedCallback was first introduced in NavigationGraph2.

The example here is not something that you will see in those tutorials even though most of them support the Predictive Back Gesture on Android 13 devices. 

*This example demonstrates how to use an OnBackPressedCallback if you are forced to use some of the techniques I’ve already mentioned above.*

This project is deliberately simple to best illustrate how the Predictive Back Gesture works. There is only a single activity with a simplistic layout consisting of a single “Hello Android!” TextView, basically the standard app as generated by net7.0-android or the equivalent in Xamarin.Android using its blank app template. The theme is based on material components and a mandatory splash screen is included.

In the MainActivity’s OnCreate we first test for an Android 13 device using gesture navigation using the utility function ```NavigationBarUtils.GetNavigationBarInteractionMode```. If it is using gesture navigation then we **don’t create an OnBackPressedCallback** and therefore we will always get the Predictive Back Gesture when exiting the app.

For all other devices including Android 13 devices using 3-button navigation, we create the callback. 

The OnBackPressedCallback class is an abstract class, so we must write our own class inheriting from OnBackPressedCallback. Our class is instantiated in the MainActivity passing the activity in the constructor. 

Inside our class, we pass a bool enabled = true to the base class to enable the callback. Finally, in the MainActivity we use AndroidX.Activity’s OnBackPressedDispatcher’s AddCallback method to add our OnBackPressedCallback to the activity. 

An OnBackPressedCallback must supply an HandleOnBackPressed() method which will be called for back gestures or back key presses. In this method, we check for the navigationMode to supply the appropriate string to the Toast message. We then remove the callback (disabling it) so it can’t be called again and therefore on the next back press or swipe the app will exit. Of course, you could modify the HandleOnBackPressed to be more sophisticated as mentioned previously with dialogs and double taps etc.

To see how OnBackPressedCallbacks are used in Fragments, please examine any of the NavigationGraph(x) tutorials. The most recent one is NavigationGraph7Net7.

**Important Note:**

One of the problems I discovered when first using the NavigationComponent was that unless I called Activity.Finish() in the HandleOnBackPressed() of the Start Destination fragment, Activity.IsFinshing would always be false in the OnDestroy of the MainActivity which would then prevent a bound service from shutting down as the app was exited. However, since the StartDestination needs to have its OnBackPressedCallback disabled to support the Predictive Back Gesture I had to find a new way to be able to ensure that the bound service would be shut down on exit.

You will also find in this project another class PredictiveBackGestureApplication which inherits from Android’s ApplicationClass. The Application class supports the Application.IActivityLifecycleCallbacks. You can see there how I now call Activity.Finish() in the method below, which ensures that the service is shut down on exit while still maintaining the Predictive Back Gesture animation.

```
public void OnActivityDestroyed(Activity activity)
{
	if (!activity.IsChangingConfigurations)
		activity.Finish();
}
```

In conclusion, I would like to stress that these techniques as demonstrated in the OnBackPressedCallback are not a recommendation and should only be used as a last resort on any Android 10 and above device which all support gesture navigation.






