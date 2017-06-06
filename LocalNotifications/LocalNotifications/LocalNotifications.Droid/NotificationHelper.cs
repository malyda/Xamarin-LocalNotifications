using System;
using System.Collections.Generic;
using System.Globalization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using LocalNotifications.Droid;
using Xamarin.Forms;
using Application = Android.App.Application;
using Android.Support.V4.App;

[assembly: Dependency(typeof(NotificationHelper))]

namespace LocalNotifications.Droid
{
    public class NotificationHelper : INotificationHelper
    {
        Context _context = Application.Context;

        /// <summary>
        /// Notification identification key
        /// </summary>
        public const string IntentDataKey = "key";
        
        /// <summary>
        ///  Notification ID
        /// </summary>
        const int NotificationId = 0;

        public void Notify(string title, string body)
        {
            Intent intent = new Intent(_context, typeof(MainActivity));

            // Create a PendingIntent; we're only using one PendingIntent (ID = 0):
            const int pendingIntentId = 0;
            PendingIntent pendingIntent = PendingIntent.GetActivity(_context, pendingIntentId, intent, PendingIntentFlags.OneShot);


            // Instantiate the builder and set notification elements:

            Notification.Builder builder = new Notification.Builder(_context)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(body)
                .SetDefaults(NotificationDefaults.All)
                .SetSmallIcon(Resource.Drawable.abc_ic_menu_overflow_material)
                .SetAutoCancel(true);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                _context.GetSystemService(Context.NotificationService) as NotificationManager;


            // Publish the notification:

            notificationManager.Notify(NotificationId, notification);
        }


        public void Notify(string title, string body, Dictionary<string, string> data)
        {
            Intent intent = new Intent(_context, typeof(MainActivity));

            PendingIntent pendingIntent = PendingIntent.GetActivity(_context, NotificationId, intent, PendingIntentFlags.OneShot);

            // Add data to Bundle transfer object
            Bundle valuesForActivity = new Bundle();
            foreach (var item in data)
            {
                valuesForActivity.PutString(item.Key, item.Value);
            }

            intent.PutExtra(IntentDataKey, valuesForActivity);

            // When the user clicks the notification, MainActivity will start up and catche Intent
            Intent resultIntent = new Intent(_context, typeof(MainActivity));

            // Pass some values to started Activity:
            resultIntent.PutExtra(IntentDataKey, valuesForActivity);

            // Construct a back stack for cross-task navigation:
            Android.App.TaskStackBuilder stackBuilder = Android.App.TaskStackBuilder.Create(_context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:            
            PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);

            // Build the notification:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(_context)
                .SetAutoCancel(true)                    // Dismiss from the notif. area when clicked
                .SetContentIntent(resultPendingIntent)  // Start 2nd activity when the intent is clicked.
                .SetContentTitle(title)      // Set its title
                .SetNumber(4)                       // Display the count in the Content Info
                .SetSmallIcon(Resource.Drawable.abc_ic_menu_overflow_material)  // Display this icon
                .SetContentText(body); // The message to display.

            // Publish the notification:
            NotificationManager notificationManager = (NotificationManager)_context.GetSystemService(Context.NotificationService);
            notificationManager.Notify(NotificationId, builder.Build());
        }
        /// <summary>
        /// Sets AlarmManager when to fire notification
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="when"></param>
        public void NotifyWhen(string title, string body, DateTime when)
        {
            Intent alarmIntent = new Intent(Forms.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("message", body);
            alarmIntent.PutExtra("title", title);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);

            alarmManager.Set(AlarmType.RtcWakeup, when.Millisecond, pendingIntent);
        }
    }
    
}