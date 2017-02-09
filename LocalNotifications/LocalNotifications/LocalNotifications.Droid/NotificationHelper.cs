using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;

using LocalNotifications.Droid;
using Xamarin.Forms;
using Application = Android.App.Application;

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

            Bundle b = new Bundle();
            foreach (var item in data)
            {
                b.PutString(item.Key, item.Value);
            }

            intent.PutExtra(IntentDataKey, b);

            // Create a PendingIntent; we're only using one PendingIntent (ID = 0):
            PendingIntent pendingIntent = PendingIntent.GetActivity(_context, NotificationId, intent, PendingIntentFlags.OneShot);


            // Instantiate the builder and set notification elements:

            Notification.Builder builder = new Notification.Builder(_context)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(body)
                .SetDefaults(NotificationDefaults.All)
                .SetSmallIcon(Resource.Drawable.abc_ic_menu_overflow_material)
                .SetAutoCancel(true);

            // builder.SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis());
            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                _context.GetSystemService(Context.NotificationService) as NotificationManager;


            // Publish the notification:

            notificationManager.Notify(NotificationId, notification);
        }
    }
    
}