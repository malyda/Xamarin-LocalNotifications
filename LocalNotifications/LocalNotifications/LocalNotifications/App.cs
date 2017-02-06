using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using LocalNotifications.Helpers;
using Plugin.LocalNotifications;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;

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
                            Text = "Show notification",
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

        Command ShowNotifi()
        {
            return new Command((() =>
            {
                Dictionary<string,string> data = new Dictionary<string, string>();
                data.Add("data1","value");
                data.Add("data2", "value2");

             
      

                //  DependencyService.Get<INotificationHelper>().Notify("title","body",DateTime.Now.AddMinutes(5),data);
                CrossLocalNotifications.Current.Show("You've got mail", "You have 793 unread messages!");
              
            }));
        }
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
            label.Text = Settings.UserName;
            Settings.UserName = "asdasdasd";
        }
    }
}
