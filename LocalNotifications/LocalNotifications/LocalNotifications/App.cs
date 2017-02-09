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
        Label label = new Label();
        Button b;
    public App()
        {
           b = new Button
           {
               Text = "Show notification after 5 secs,",
               Command = ShowNotifi(DateTime.Now.AddSeconds(5))
           };
            // The root page of your application

            StackLayout layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    new Label
                    {
                        Text = "Crossplatform notification"
                    },
                        new Button
                        {
                            Text = "Show crossplatform notification",
                            Command = ShowNotifi()
                        },
                        label

                }
            };
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
                new Button
                {
                    Text = "Android delayed notification",
                    Command = ShowNotifi(DateTime.Now.AddSeconds(5))
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
                CrossLocalNotifications.Current.Show("You've got mail", "You have 793 unread messages!"); 
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
                DependencyService.Get<INotificationHelper>().Notify("title","body",data);
            }));
        }
        /// <summary>
        /// Shows crossplatform notification at some time
        /// By Plugin.LocalNotificatins limit it is working only with Android
        /// </summary>
        /// <param name="date">When notification fire</param>
        /// <returns></returns>
        Command ShowNotifi(DateTime date)
        {
            return new Command((() =>
            {
                
                Debug.WriteLine(date);
                CrossLocalNotifications.Current.Show("Good morning", "Time to get up!", 1, date);
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
