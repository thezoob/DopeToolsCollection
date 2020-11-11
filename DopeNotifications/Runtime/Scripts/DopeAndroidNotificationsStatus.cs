using Ludiq;
using Bolt;
using Unity.Notifications.Android;


    [UnitSurtitle("Dope Notifications Android")]
    [UnitTitle("Check Status")]
    [UnitCategory("DopeNotifications/Android")]

    [Inspectable]
    public sealed class DopeAndroidNotificationsStatus : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput enter;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        [PortLabelHidden]
        [PortLabel("id")]
        public ValueInput Identifier;

        //[DoNotSerialize]
        //public ControlOutput Scheduled;

        //[DoNotSerialize]
        //public ControlOutput Delivered;

        //[DoNotSerialize]
        //public ControlOutput Unknown;


        [UnitHeaderInspectable]
        public AndroidStatusType androidStatus;

        private int id;

        protected override void Definition()
        {
            enter = ControlInput("enter", (flow) =>
            {

                id = flow.GetValue<int>(Identifier);

                

                if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled && androidStatus == AndroidStatusType.Scheduled)
                {
                    // Replace the currently scheduled notification with a new notification.
                    //AndroidNotificationCenter.UpdateScheduledNotification(identifier, newNotification, "channel_id");
                    return exit;
                }
                else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Delivered && androidStatus == AndroidStatusType.Delivered)
                {
                    //Remove the notification from the status bar
                    //AndroidNotificationCenter.CancelNotification(id);
                    return exit;
                }
                else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Unknown && androidStatus == AndroidStatusType.Unknown)
                {
                    //AndroidNotificationCenter.SendNotification(newNotification, "channel_id");
                    return exit;
                }

                return exit;
            });

            exit = ControlOutput("exit");
            //Scheduled = ControlOutput("Scheduled");
            //Delivered = ControlOutput("Delivered");
            //Unknown = ControlOutput("Unknown");

            Identifier = ValueInput<int>("Identifier");


            Requirement(Identifier, enter);
            Succession(enter, exit);
            //Succession(enter, Scheduled);
            //Succession(enter, Delivered);
            //Succession(enter, Unknown);

        }


    }


