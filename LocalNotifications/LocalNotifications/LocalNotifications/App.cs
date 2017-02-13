using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Plugin.LocalNotifications;

using Xamarin.Forms;
using Android.Content;
using Android.OS;
using Application = Xamarin.Forms.Application;
using Debug = System.Diagnostics.Debug;

namespace LocalNotifications
{
    public class App : Application
    {
    public App()
        {
            StackLayout layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    new Label
                    {

                        Text = "Crossplatform notification"
                    },
                        new Button
                        {
                            Text = "Show crossplatform notification",
                            Command = ShowNotifi()
                        }
                }
            };
            // Part showing only on Android
            if(Device.OS == TargetPlatform.Android)
            {
                layout.Children.Add(
                new Label
                {
                    Text = "Android Only notification"
                });
                layout.Children.Add(
                new Button
                {
                    Text = "Native Android Notification",
                    Command = ShowNotifiAndroidNative()
                });

                layout.Children.Add(
                new Label
                {
                    Text = "Notifications fire after 5 secs.",
                });
                layout.Children.Add(
                new Button
                {
                    Text = "Android delayed notification",
                    Command = ShowNotifi(DateTime.Now.AddSeconds(5))
                });
                layout.Children.Add(
                 new Button
                 {
                     Text = "Android delayed notification native",
                     Command = ShowNotifiAndroidNativeDelayed(DateTime.Now.AddMilliseconds(3000))
                 });
            }
            
            var content = new ContentPage
            {
                Title = "LocalNotifications",
                Content = layout
            };
            
            MainPage = new NavigationPage(content);                
        }

        /// <summary>
        /// Show crossplatform notification
        /// </summary>
        /// <returns></returns>
        Command ShowNotifi()
        {
            return new Command((() =>
            {
                CrossLocalNotifications.Current.Show("Crossplatform notification", "Body text"); 
            }));
        }
        /// <summary>
        /// Show native Android notification and pass data to it
        /// </summary>
        /// <returns></returns>
        Command ShowNotifiAndroidNative()
        {
            return new Command((() =>
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("data1", "value");
                data.Add("data2", "value2");

                DependencyService.Get<INotificationHelper>().Notify("Android native notification","With data",data);
                
            }));
        }
        /// <summary>
        /// Shows crossplatform notification at some time
        /// By Plugin.LocalNotifications limit, it is working only with Android
        /// </summary>
        /// <param name="date">When notification fire</param>
        /// <returns></returns>
        Command ShowNotifi(DateTime date)
        {
            return new Command((() =>
            {
                
                Debug.WriteLine(date);
                CrossLocalNotifications.Current.Show("Android delayed notification", "From Plugin.LocalNotifications", 1, date);
            }));
        }

        /// <summary>
        /// Fires Android native notification at scheduled time
        /// </summary>
        /// <param name="when">DateTime when to fire notification</param>
        /// <returns></returns>
        Command ShowNotifiAndroidNativeDelayed(DateTime when)
        {
            return new Command((() =>
            {
                DependencyService.Get<INotificationHelper>().NotifyWhen("Android native delayed notification", "Using native Android AlarmManager", when);
            }));
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes

        }
    }
}
