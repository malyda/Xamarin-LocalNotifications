using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace LocalNotifications.Droid
{
    [Activity(Label = "LocalNotifications", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());


            if (Intent.HasExtra(NotificationHelper.IntentDataKey))
            {
                ProcessNotificationData();
            }
        }

        /// <summary>
        /// Get data from notification and show ReceiverPage
        /// </summary>
        public void ProcessNotificationData()
        {
          //  Console.WriteLine(Intent.Extras.GetBundle(NotificationHelper.IntentDataKey).GetString("data1"));

            // Get data from notification as Bundle object
            Bundle bundleFromNotification = Intent.Extras.GetBundle(NotificationHelper.IntentDataKey);

            Dictionary<string, string> data = new Dictionary<string, string>();

            // Copy data from bundle to Dictionary
            foreach (var key in bundleFromNotification.KeySet())
            {
                data.Add(key, bundleFromNotification.GetString(key));
            }

            // Replace actual Page with ReceiverPage and pass data
            Xamarin.Forms.Application.Current.MainPage = new ReceiverPage(data);
        }
    }
}

