using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNotifications
{
    public interface INotificationHelper
    {
        void Notify(string title, string body);
      
        void Notify(string title, string body, Dictionary<string, string> data);

    }
}
