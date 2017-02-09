using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNotifications
{
    public interface INotificationHelper
    {
        /// <summary>
        /// Show notification
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        void Notify(string title, string body);
        
        /// <summary>
        /// Show notification and pass data
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        void Notify(string title, string body, Dictionary<string, string> data);

    }
}
