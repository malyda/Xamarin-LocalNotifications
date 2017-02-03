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
        public App()
        {
           
            // The root page of your application

            var content = new ContentPage
            {
                Title = "LocalNotifications",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Button
                        {
                            Text = "Show notification",
                            Command = ShowNotifi()
                        },
                        new Button
                        {
                            Text = "Show notification after 5 secs,",
                            Command = ShowNotifi(DateTime.Now.AddSeconds(5))
                        },
                        label
                    }
                }
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
        Command ShowNotifi(DateTime date)
        {
            return new Command((() =>
            {
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
