using Ludiq;
using Bolt;
using Unity.Notifications.Android;

[UnitSurtitle("Dope Notifications Android")]
[UnitTitle("Send Delayed Notification")]
[UnitCategory("DopeTools/Notifications/Android")]
[RenamedFrom("DopeNotifications")]

    public sealed class DopeAndroidNotifications : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput enter;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        [DoNotSerialize]
        public ValueInput Text;

        [DoNotSerialize]
        public ValueInput Title;

        [DoNotSerialize]
        public ValueInput Delay;

        [DoNotSerialize]
        public ValueInput ImportanceValue;

        [DoNotSerialize]
        [PortLabelHidden]
        [PortLabel("id")]
        public ValueOutput Notification;


        private float d = 0f;
        private Importance iV = Importance.High;
        private string title;
        private string text;
        private DopeAndroidNotify dope = new DopeAndroidNotify();

        private AndroidNotification notification;

        protected override void Definition()
        {
            enter = ControlInput("enter", (Flow flow) =>
            {
                d = flow.GetValue<float>(Delay);
                iV = flow.GetValue<Importance>(ImportanceValue);
                title = flow.GetValue<string>(Title);
                text = flow.GetValue<string>(Text);
                return exit;

            });


            exit = ControlOutput("exit");

            Title = ValueInput("Title", string.Empty);
            Text = ValueInput("Text", string.Empty);
            Delay = ValueInput("Delay", 0f);
            ImportanceValue = ValueInput("Importance", Importance.High);

            Notification = ValueOutput<int>("Notification", (Flow flow) => { return dope.InvokeAndroidNotification(title, text, iV, d); });

            Succession(enter, exit);
        }

        //private int InvokeAndroidNotification()
        //{
        //    var c = new AndroidNotificationChannel()
        //    {
        //        Id = "channel_id",
        //        Name = "Default Channel",
        //        Importance = iV,
        //        Description = "Generic notifications",
        //    };
        //    AndroidNotificationCenter.RegisterNotificationChannel(c);

        //    var notification = new AndroidNotification
        //    {
        //        Title = title,
        //        Text = text,
        //        FireTime = System.DateTime.Now.AddSeconds(d)
        //    };

        //    var anc = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        //    return anc;
        //}
    }
