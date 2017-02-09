using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace LocalNotifications
{
    public class ReceiverPage : ContentPage
    {
        ListView _listView = new ListView();
        /// <summary>
        /// Show blank page
        /// </summary>
        public ReceiverPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello Page" }
                }
            };
        }
        /// <summary>
        /// Show page with listview which contains data from notification
        /// </summary>
        /// <param name="data"></param>
        public ReceiverPage(Dictionary<string, string> data)
        {
           
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Data from notification" },
                    _listView
                }
            };
            _listView.ItemsSource = data;
        }
    }
}
