using System;

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
               // Console.WriteLine(Intent.Extras.GetBundle("key").GetString("hello"));
                App.Current.MainPage = new ReceiverPage();
            }

        }

    }
}

