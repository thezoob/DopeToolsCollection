#if UNITY_ANDROID
using Unity.Notifications.Android;

namespace Dopetools.DopeNotifications
{

    public class DopeAndroidNotify
    {
        //Invoke Notification after Delay in Seconds
        public int InvokeAndroidNotification(string title, string text, Importance iV, float d)
        {
            var c = new AndroidNotificationChannel()
            {
                Id = "channel_id",
                Name = "Default Channel",
                Importance = iV,
                Description = "Generic notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(c);

            var notification = new AndroidNotification
            {
                Title = title,
                Text = text,
                FireTime = System.DateTime.Now.AddSeconds(d)
            };

            var anc = AndroidNotificationCenter.SendNotification(notification, "channel_id");

            return anc;
        }

        //Invoke Notification at Specified Time

        public int InvokeAndroidNotification(string title, string text, Importance iV, string dateTime)
        {
            var c = new AndroidNotificationChannel()
            {
                Id = "channel_id",
                Name = "Default Channel",
                Importance = iV,
                Description = "Generic notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(c);

            var notification = new AndroidNotification
            {
                Title = title,
                Text = text,
                FireTime = System.DateTime.Parse(dateTime)
            };

            var anc = AndroidNotificationCenter.SendNotification(notification, "channel_id");

            return anc;
        }

    }

}

#endif