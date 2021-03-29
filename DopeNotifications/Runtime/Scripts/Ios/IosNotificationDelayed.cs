#if UNITY_IOS
using UnityEngine;
using Unity.VisualScripting;
using Unity.Notifications.iOS;
using System;

namespace Dopetools.DopeNotifications
{

    [UnitSurtitle("Dope Notifications iOS")]
    [UnitTitle("Send Delayed Notification")]
    [UnitCategory("DopeTools/Notifications/iOS")]
    [RenamedFrom("IosNotificationDelayed")]
    [RenamedFrom("DopeIosNotificationDelayed")]

    public sealed class DopeIosNotificationDelayed : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput Enter;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput Exit;

        [DoNotSerialize]
        public ValueInput Delay;

        [DoNotSerialize]
        public ValueInput Title;

        [DoNotSerialize]
        public ValueInput SubTitle;

        [DoNotSerialize]
        public ValueInput Body;

        [DoNotSerialize]
        public ValueOutput Identifier;

        private int delayValue;
        private string id;

        protected override void Definition()
        {
            Enter = ControlInput("Enter", (flow) =>
            {
                delayValue = flow.GetValue<int>(Delay);

                return Exit;
            });


            Exit = ControlOutput("Exit");
            Delay = ValueInput<int>("Delay", 0);
            Title = ValueInput<string>("Title", string.Empty);
            SubTitle = ValueInput<string>("SubTitle", string.Empty);
            Body = ValueInput<string>("Body", string.Empty);

            Identifier = ValueOutput<string>("id", (flow) =>
            {
                id = IosNotify(flow.GetValue<int>(Delay), flow.GetValue<string>(Title), flow.GetValue<string>(SubTitle), flow.GetValue<string>(Body));
                return id;
            });

            Requirement(Title, Enter);
            Requirement(Body, Enter);

            Succession(Enter, Exit);

        }

        private string IosNotify(int delay, string title, string subtitle, String body)
        {
            var timeTrigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = new TimeSpan(0, 0, delayValue),
                Repeats = false
            };

            var notification = new iOSNotification()
            {
                // You can specify a custom identifier which can be used to manage the notification later.
                // If you don't provide one, a unique string will be generated automatically.

                Title = title,
                Body = body,
                Subtitle = subtitle,
                ShowInForeground = true,
                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                CategoryIdentifier = "category_a",
                ThreadIdentifier = "thread1",
                Trigger = timeTrigger,

            };
            iOSNotificationCenter.ScheduleNotification(notification);
            return notification.Identifier;

        }
    }

}
#endif