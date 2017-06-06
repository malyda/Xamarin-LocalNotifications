using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace LocalNotifications.Droid
{
    [BroadcastReceiver]
    [IntentFilter(new string[] { "android.intent.action.BOOT_COMPLETED" }, Priority = (int)IntentFilterPriority.LowPriority)]
    class AlarmReceiver :BroadcastReceiver
    {
        /// <summary>
        /// Method called after AlarmManager fires, or device is booted
        /// </summary>
        /// <param name="context"></param>
        /// <param name="intent">Data for delayed notification, if AlarmManager fires it
        /// Or null if Broadcast is received</param>
        public override void OnReceive(Context context, Intent intent)
        {
            string title = "Title from receiver";
            string body = "Body from receiver";
            
            if (intent.Action != Intent.ActionBootCompleted)
            {
                body = intent.GetStringExtra("message");
                title = intent.GetStringExtra("title");
                NotificationHelper notificationHelper = new NotificationHelper();
                notificationHelper.Notify(title, body);
            }
            else
            {
                // Check for news, register notifications, start some action
            }
           
     
        }
    }
}