#if UNITY_ANDROID
using Unity.VisualScripting;
using Unity.Notifications.Android;

namespace Dopetools.DopeNotifications
{
    [UnitSurtitle("Dope Notifications Android")]
    [UnitTitle("Check Status")]
    [UnitCategory("DopeTools/Notifications/Android")]
    [RenamedFrom("DopeAndroidNotificationsStatus")]

    public sealed class DopeAndroidNotificationsStatus : Unit
    {

        //constructor
        public DopeAndroidNotificationsStatus() : base() { }

        //constructor
        public DopeAndroidNotificationsStatus(AndroidStatusType androidStatusType) : base()
        {
            this.androidStatusType = androidStatusType;
        }
        //constructor
        public DopeAndroidNotificationsStatus(DopeAndroidNotificationsStatus dopeAndroidNotificationsStatus)
        {
            this.dopeAndroidNotificationsStatus = dopeAndroidNotificationsStatus;
        }

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput enter;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        [PortLabelHidden]
        [PortLabel("id")]
        public ValueInput Identifier;

        [UnitHeaderInspectable]
        [InspectorToggleLeft]
        public AndroidStatusType androidStatusType;

        private DopeAndroidNotificationsStatus dopeAndroidNotificationsStatus;

        private int id;

        protected override void Definition()
        {
            enter = ControlInput("enter", (flow) =>
            {

                id = flow.GetValue<int>(Identifier);



                if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled && androidStatusType == AndroidStatusType.Scheduled)
                {
                    // Replace the currently scheduled notification with a new notification.
                    //AndroidNotificationCenter.UpdateScheduledNotification(identifier, newNotification, "channel_id");
                    return exit;
                }
                else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Delivered && androidStatusType == AndroidStatusType.Delivered)
                {
                    //Remove the notification from the status bar
                    //AndroidNotificationCenter.CancelNotification(id);
                    return exit;
                }
                else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Unknown && androidStatusType == AndroidStatusType.Unknown)
                {
                    //AndroidNotificationCenter.SendNotification(newNotification, "channel_id");
                    return exit;
                }

                return exit;
            });

            exit = ControlOutput("exit");

            Identifier = ValueInput<int>("Identifier");

            Requirement(Identifier, enter);
            Succession(enter, exit);

        }


    }


}

#endif