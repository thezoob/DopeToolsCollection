using UnityEngine;
using Bolt;
using Ludiq;
using Unity.Notifications.iOS;
using System;

namespace Dopetools.DopeNotifications
{

    [UnitSurtitle("Dope Notifications iOS")]
    [UnitTitle("Send Calendar Notification")]
    [UnitCategory("DopeTools/Notifications/iOS")]
    [RenamedFrom("DopeIosNotificationCalendar")]

    public sealed class DopeIosNotificationCalendar : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput Enter;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput Exit;

        [DoNotSerialize]
        public ValueInput DateTimeValue;

        [DoNotSerialize]
        public ValueInput Title;

        [DoNotSerialize]
        public ValueInput SubTitle;

        [DoNotSerialize]
        public ValueInput Body;

        [DoNotSerialize]
        public ValueInput RepeatsValue;


        [DoNotSerialize]
        public ValueOutput Identifier;


        private string id;
        private int result;

        protected override void Definition()
        {
            Enter = ControlInput("Enter", (flow) =>
            {
                var now = DateTime.Now;
                var inputDateAndTime = DateTime.Parse(flow.GetValue<string>(DateTimeValue));
                result = DateTime.Compare(now, inputDateAndTime);

              
                    return Exit;
            });


            Exit = ControlOutput("Exit");

            Title = ValueInput<string>("Title", string.Empty);
            SubTitle = ValueInput<string>("SubTitle", string.Empty);
            Body = ValueInput<string>("Body", string.Empty);
            DateTimeValue = ValueInput<string>("Date and Time", "12/25/2020 2:30:00 PM");
            RepeatsValue = ValueInput<bool>("Repeat", false);

            Identifier = ValueOutput<string>("id", (flow) =>
            {
                if (result > 0)
                {
                    id = "Schedule Date and Time cannot be before Now. Enter Date and Time after now.";
                }
                else
                {
                    id = IosNotify(flow.GetValue<string>(DateTimeValue), flow.GetValue<string>(Title), flow.GetValue<string>(SubTitle), flow.GetValue<string>(Body), flow.GetValue<bool>(RepeatsValue));
                }
                    return id;
            });

            //Requirement(Title, Enter);
            //Requirement(Body, Enter);
            //Requirement(DateTimeValue, Enter);

            Succession(Enter, Exit);

        }

        private string IosNotify(string dateTime, string title, string subtitle, String body, bool repeats)
        {
            var dateNotify = DateTimeToNotify(dateTime);

            var calendarTrigger = new iOSNotificationCalendarTrigger()
            {
                Year = dateNotify.Year,
                Month = dateNotify.Month,
                Day = dateNotify.Day,
                Hour = dateNotify.Hour,
                Minute = dateNotify.Minute,
                Second = dateNotify.Second,
                Repeats = repeats
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
                Trigger = calendarTrigger,

            };
            iOSNotificationCenter.ScheduleNotification(notification);
            return notification.Identifier;

        }

        private DateTime DateTimeToNotify(string dateTimeInputValue)
        {
            var parsedDateTime = DateTime.Parse(dateTimeInputValue);
            return parsedDateTime;
        }
    }

} //namespace