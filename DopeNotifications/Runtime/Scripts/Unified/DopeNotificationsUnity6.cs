#if DOPETOOLS_HAS_MOBILE_NOTIFICATIONS
using System;
using System.Collections;
using Bolt;
using Ludiq;
using Unity.Notifications;
using UnityEngine;

namespace Dopetools.DopeNotifications
{
    /// <summary>
    /// Unity 6 / Mobile Notifications 2.3+ unified notification bridge.
    /// Works across Android and iOS via Unity.Notifications.NotificationCenter.
    /// </summary>
    public static class DopeNotifications
    {
        public const string DefaultChannelId = "dope_default";
        public const string DefaultChannelName = "Dope Notifications";
        public const string DefaultChannelDescription = "Notifications scheduled from DopeTools.";

        private static bool initialized;

        public static bool IsInitialized => initialized;

        public static void Initialize(string channelId, string channelName, string channelDescription)
        {
            var args = NotificationCenterArgs.Default;
            args.AndroidChannelId = string.IsNullOrEmpty(channelId) ? DefaultChannelId : channelId;
            args.AndroidChannelName = string.IsNullOrEmpty(channelName) ? DefaultChannelName : channelName;
            args.AndroidChannelDescription = string.IsNullOrEmpty(channelDescription) ? DefaultChannelDescription : channelDescription;
            args.PresentationOptions = NotificationPresentation.Alert | NotificationPresentation.Sound | NotificationPresentation.Badge;

            NotificationCenter.Initialize(args);
            initialized = true;
        }

        public static void EnsureInitialized()
        {
            if (!initialized)
            {
                Initialize(DefaultChannelId, DefaultChannelName, DefaultChannelDescription);
            }
        }

        public static NotificationsPermissionRequest RequestPermission()
        {
            EnsureInitialized();
            return NotificationCenter.RequestPermission();
        }

        public static int ScheduleDelayed(string title, string text, double delaySeconds, bool repeats, bool showInForeground, int badge, string data, int identifier = 0)
        {
            EnsureInitialized();

            var notification = CreateNotification(title, text, showInForeground, badge, data, identifier);
            var schedule = new NotificationIntervalSchedule(TimeSpan.FromSeconds(Math.Max(1d, delaySeconds)), repeats);
            return NotificationCenter.ScheduleNotification(notification, schedule);
        }

        public static int ScheduleAt(string title, string text, DateTime fireTime, NotificationRepeatInterval repeatInterval, bool showInForeground, int badge, string data, int identifier = 0)
        {
            EnsureInitialized();

            var notification = CreateNotification(title, text, showInForeground, badge, data, identifier);
            var schedule = new NotificationDateTimeSchedule(fireTime, repeatInterval);
            return NotificationCenter.ScheduleNotification(notification, schedule);
        }

        public static void CancelScheduled(int identifier)
        {
            NotificationCenter.CancelScheduledNotification(identifier);
        }

        public static void CancelDelivered(int identifier)
        {
            NotificationCenter.CancelDeliveredNotification(identifier);
        }

        public static void CancelAllScheduled()
        {
            NotificationCenter.CancelAllScheduledNotifications();
        }

        public static void CancelAllDelivered()
        {
            NotificationCenter.CancelAllDeliveredNotifications();
        }

        public static void ClearBadge()
        {
            NotificationCenter.ClearBadge();
        }

        public static void OpenSettings()
        {
            NotificationCenter.OpenNotificationSettings(NotificationSettingsSection.Application);
        }

        private static Notification CreateNotification(string title, string text, bool showInForeground, int badge, string data, int identifier)
        {
            var notification = new Notification
            {
                Title = title,
                Text = text,
                ShowInForeground = showInForeground,
                Data = data
            };

            if (badge > 0)
            {
                notification.Badge = badge;
            }

            if (identifier > 0)
            {
                notification.Identifier = identifier;
            }

            return notification;
        }
    }

    [UnitSurtitle("Dope Notifications")]
    [UnitTitle("Initialize")]
    [UnitCategory("DopeTools/Notifications")]
    public sealed class DopeNotificationsInitialize : Unit
    {
        [DoNotSerialize, PortLabelHidden] public ControlInput enter { get; private set; }
        [DoNotSerialize, PortLabelHidden] public ControlOutput exit { get; private set; }
        [DoNotSerialize] public ValueInput channelId { get; private set; }
        [DoNotSerialize] public ValueInput channelName { get; private set; }
        [DoNotSerialize] public ValueInput channelDescription { get; private set; }

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Enter);
            exit = ControlOutput(nameof(exit));
            channelId = ValueInput<string>(nameof(channelId), DopeNotifications.DefaultChannelId);
            channelName = ValueInput<string>(nameof(channelName), DopeNotifications.DefaultChannelName);
            channelDescription = ValueInput<string>(nameof(channelDescription), DopeNotifications.DefaultChannelDescription);

            Requirement(channelId, enter);
            Requirement(channelName, enter);
            Requirement(channelDescription, enter);
            Succession(enter, exit);
        }

        private ControlOutput Enter(Flow flow)
        {
            DopeNotifications.Initialize(
                flow.GetValue<string>(channelId),
                flow.GetValue<string>(channelName),
                flow.GetValue<string>(channelDescription));
            return exit;
        }
    }

    [UnitSurtitle("Dope Notifications")]
    [UnitTitle("Request Permission")]
    [UnitCategory("DopeTools/Notifications")]
    public sealed class DopeNotificationsRequestPermission : Unit
    {
        [DoNotSerialize, PortLabelHidden] public ControlInput enter { get; private set; }
        [DoNotSerialize] public ControlOutput granted { get; private set; }
        [DoNotSerialize] public ControlOutput denied { get; private set; }
        [DoNotSerialize] public ControlOutput finished { get; private set; }
        [DoNotSerialize] public ValueOutput status { get; private set; }

        protected override void Definition()
        {
            enter = ControlInputCoroutine(nameof(enter), Request);
            granted = ControlOutput(nameof(granted));
            denied = ControlOutput(nameof(denied));
            finished = ControlOutput(nameof(finished));
            status = ValueOutput<NotificationsPermissionStatus>(nameof(status));

            Assignment(enter, status);
            Succession(enter, granted);
            Succession(enter, denied);
            Succession(enter, finished);
        }

        private IEnumerator Request(Flow flow)
        {
            var request = DopeNotifications.RequestPermission();

            if (request.Status == NotificationsPermissionStatus.RequestPending)
            {
                yield return request;
            }

            flow.SetValue(status, request.Status);

            if (request.Status == NotificationsPermissionStatus.Granted)
            {
                yield return granted;
            }
            else
            {
                yield return denied;
            }

            yield return finished;
        }
    }

    [UnitSurtitle("Dope Notifications")]
    [UnitTitle("Schedule Delayed")]
    [UnitCategory("DopeTools/Notifications")]
    [RenamedFrom("Dopetools.DopeNotifications.DopeAndroidNotifications")]
    [RenamedFrom("Dopetools.DopeNotifications.DopeIosNotificationDelayed")]
    public sealed class DopeNotificationsScheduleDelayed : Unit
    {
        [DoNotSerialize, PortLabelHidden] public ControlInput enter { get; private set; }
        [DoNotSerialize, PortLabelHidden] public ControlOutput exit { get; private set; }
        [DoNotSerialize] public ValueInput title { get; private set; }
        [DoNotSerialize] public ValueInput text { get; private set; }
        [DoNotSerialize] public ValueInput delaySeconds { get; private set; }
        [DoNotSerialize] public ValueInput repeats { get; private set; }
        [DoNotSerialize] public ValueInput showInForeground { get; private set; }
        [DoNotSerialize] public ValueInput badge { get; private set; }
        [DoNotSerialize] public ValueInput data { get; private set; }
        [DoNotSerialize] public ValueInput identifier { get; private set; }
        [DoNotSerialize] public ValueOutput scheduledId { get; private set; }

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Enter);
            exit = ControlOutput(nameof(exit));
            title = ValueInput<string>(nameof(title), string.Empty);
            text = ValueInput<string>(nameof(text), string.Empty);
            delaySeconds = ValueInput<float>(nameof(delaySeconds), 1f);
            repeats = ValueInput<bool>(nameof(repeats), false);
            showInForeground = ValueInput<bool>(nameof(showInForeground), true);
            badge = ValueInput<int>(nameof(badge), 0);
            data = ValueInput<string>(nameof(data), string.Empty);
            identifier = ValueInput<int>(nameof(identifier), 0);
            scheduledId = ValueOutput<int>(nameof(scheduledId));

            Requirement(title, enter);
            Requirement(text, enter);
            Requirement(delaySeconds, enter);
            Requirement(repeats, enter);
            Requirement(showInForeground, enter);
            Requirement(badge, enter);
            Requirement(data, enter);
            Requirement(identifier, enter);
            Assignment(enter, scheduledId);
            Succession(enter, exit);
        }

        private ControlOutput Enter(Flow flow)
        {
            var id = DopeNotifications.ScheduleDelayed(
                flow.GetValue<string>(title),
                flow.GetValue<string>(text),
                flow.GetValue<float>(delaySeconds),
                flow.GetValue<bool>(repeats),
                flow.GetValue<bool>(showInForeground),
                flow.GetValue<int>(badge),
                flow.GetValue<string>(data),
                flow.GetValue<int>(identifier));
            flow.SetValue(scheduledId, id);
            return exit;
        }
    }

    [UnitSurtitle("Dope Notifications")]
    [UnitTitle("Schedule Date Time")]
    [UnitCategory("DopeTools/Notifications")]
    [RenamedFrom("Dopetools.DopeNotifications.DopeAndroidScheduleNotification")]
    [RenamedFrom("Dopetools.DopeNotifications.DopeIosNotificationCalendar")]
    public sealed class DopeNotificationsScheduleDateTime : Unit
    {
        [DoNotSerialize, PortLabelHidden] public ControlInput enter { get; private set; }
        [DoNotSerialize, PortLabelHidden] public ControlOutput exit { get; private set; }
        [DoNotSerialize] public ValueInput title { get; private set; }
        [DoNotSerialize] public ValueInput text { get; private set; }
        [DoNotSerialize] public ValueInput dateTime { get; private set; }
        [DoNotSerialize] public ValueInput repeatInterval { get; private set; }
        [DoNotSerialize] public ValueInput showInForeground { get; private set; }
        [DoNotSerialize] public ValueInput badge { get; private set; }
        [DoNotSerialize] public ValueInput data { get; private set; }
        [DoNotSerialize] public ValueInput identifier { get; private set; }
        [DoNotSerialize] public ValueOutput scheduledId { get; private set; }

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Enter);
            exit = ControlOutput(nameof(exit));
            title = ValueInput<string>(nameof(title), string.Empty);
            text = ValueInput<string>(nameof(text), string.Empty);
            dateTime = ValueInput<string>(nameof(dateTime), DateTime.Now.AddHours(1).ToString());
            repeatInterval = ValueInput(nameof(repeatInterval), NotificationRepeatInterval.OneTime);
            showInForeground = ValueInput<bool>(nameof(showInForeground), true);
            badge = ValueInput<int>(nameof(badge), 0);
            data = ValueInput<string>(nameof(data), string.Empty);
            identifier = ValueInput<int>(nameof(identifier), 0);
            scheduledId = ValueOutput<int>(nameof(scheduledId));

            Requirement(title, enter);
            Requirement(text, enter);
            Requirement(dateTime, enter);
            Requirement(repeatInterval, enter);
            Requirement(showInForeground, enter);
            Requirement(badge, enter);
            Requirement(data, enter);
            Requirement(identifier, enter);
            Assignment(enter, scheduledId);
            Succession(enter, exit);
        }

        private ControlOutput Enter(Flow flow)
        {
            var id = DopeNotifications.ScheduleAt(
                flow.GetValue<string>(title),
                flow.GetValue<string>(text),
                DateTime.Parse(flow.GetValue<string>(dateTime)),
                flow.GetValue<NotificationRepeatInterval>(repeatInterval),
                flow.GetValue<bool>(showInForeground),
                flow.GetValue<int>(badge),
                flow.GetValue<string>(data),
                flow.GetValue<int>(identifier));
            flow.SetValue(scheduledId, id);
            return exit;
        }
    }

    [UnitSurtitle("Dope Notifications")]
    [UnitTitle("Cancel Notification")]
    [UnitCategory("DopeTools/Notifications")]
    public sealed class DopeNotificationsCancel : Unit
    {
        [DoNotSerialize, PortLabelHidden] public ControlInput enter { get; private set; }
        [DoNotSerialize, PortLabelHidden] public ControlOutput exit { get; private set; }
        [DoNotSerialize] public ValueInput identifier { get; private set; }
        [DoNotSerialize] public ValueInput delivered { get; private set; }
        [DoNotSerialize] public ValueInput scheduled { get; private set; }

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Enter);
            exit = ControlOutput(nameof(exit));
            identifier = ValueInput<int>(nameof(identifier), 0);
            delivered = ValueInput<bool>(nameof(delivered), true);
            scheduled = ValueInput<bool>(nameof(scheduled), true);

            Requirement(identifier, enter);
            Requirement(delivered, enter);
            Requirement(scheduled, enter);
            Succession(enter, exit);
        }

        private ControlOutput Enter(Flow flow)
        {
            var id = flow.GetValue<int>(identifier);
            if (flow.GetValue<bool>(scheduled)) DopeNotifications.CancelScheduled(id);
            if (flow.GetValue<bool>(delivered)) DopeNotifications.CancelDelivered(id);
            return exit;
        }
    }

    [UnitSurtitle("Dope Notifications")]
    [UnitTitle("Cancel All Notifications")]
    [UnitCategory("DopeTools/Notifications")]
    public sealed class DopeNotificationsCancelAll : Unit
    {
        [DoNotSerialize, PortLabelHidden] public ControlInput enter { get; private set; }
        [DoNotSerialize, PortLabelHidden] public ControlOutput exit { get; private set; }
        [DoNotSerialize] public ValueInput delivered { get; private set; }
        [DoNotSerialize] public ValueInput scheduled { get; private set; }

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Enter);
            exit = ControlOutput(nameof(exit));
            delivered = ValueInput<bool>(nameof(delivered), true);
            scheduled = ValueInput<bool>(nameof(scheduled), true);

            Requirement(delivered, enter);
            Requirement(scheduled, enter);
            Succession(enter, exit);
        }

        private ControlOutput Enter(Flow flow)
        {
            if (flow.GetValue<bool>(scheduled)) DopeNotifications.CancelAllScheduled();
            if (flow.GetValue<bool>(delivered)) DopeNotifications.CancelAllDelivered();
            return exit;
        }
    }

    [UnitSurtitle("Dope Notifications")]
    [UnitTitle("Open Settings")]
    [UnitCategory("DopeTools/Notifications")]
    public sealed class DopeNotificationsOpenSettings : Unit
    {
        [DoNotSerialize, PortLabelHidden] public ControlInput enter { get; private set; }
        [DoNotSerialize, PortLabelHidden] public ControlOutput exit { get; private set; }

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Enter);
            exit = ControlOutput(nameof(exit));
            Succession(enter, exit);
        }

        private ControlOutput Enter(Flow flow)
        {
            DopeNotifications.OpenSettings();
            return exit;
        }
    }

    [UnitSurtitle("Dope Notifications")]
    [UnitTitle("Notification Received")]
    [UnitCategory("DopeTools/Notifications")]
    public sealed class DopeNotificationsReceived : Unit, IGraphElementWithData, IGraphEventListener
    {
        public sealed class Data : IGraphElementData
        {
            public bool isListening;
            public Delegate callback;
            public string title = string.Empty;
            public string text = string.Empty;
            public string data = string.Empty;
            public int identifier;
        }

        [DoNotSerialize] public ControlOutput received { get; private set; }
        [DoNotSerialize] public ValueOutput title { get; private set; }
        [DoNotSerialize] public ValueOutput text { get; private set; }
        [DoNotSerialize] public ValueOutput data { get; private set; }
        [DoNotSerialize] public ValueOutput identifier { get; private set; }

        protected override void Definition()
        {
            isControlRoot = true;

            received = ControlOutput(nameof(received));
            title = ValueOutput<string>(nameof(title), GetTitle);
            text = ValueOutput<string>(nameof(text), GetText);
            data = ValueOutput<string>(nameof(data), GetData);
            identifier = ValueOutput<int>(nameof(identifier), GetIdentifier);
        }

        public IGraphElementData CreateData()
        {
            return new Data();
        }

        public void StartListening(GraphStack stack)
        {
            var graphData = stack.GetElementData<Data>(this);

            if (graphData.isListening)
            {
                return;
            }

            DopeNotifications.EnsureInitialized();

            var reference = stack.ToReference();
            Action<Notification> callback = notification => Trigger(reference, notification);
            NotificationCenter.OnNotificationReceived += callback;
            graphData.callback = callback;
            graphData.isListening = true;
        }

        public void StopListening(GraphStack stack)
        {
            var graphData = stack.GetElementData<Data>(this);

            if (!graphData.isListening)
            {
                return;
            }

            NotificationCenter.OnNotificationReceived -= (Action<Notification>)graphData.callback;
            graphData.callback = null;
            graphData.isListening = false;
        }

        public bool IsListening(GraphPointer pointer)
        {
            return pointer.GetElementData<Data>(this).isListening;
        }

        private string GetTitle(Flow flow)
        {
            return flow.stack.GetElementData<Data>(this).title;
        }

        private string GetText(Flow flow)
        {
            return flow.stack.GetElementData<Data>(this).text;
        }

        private string GetData(Flow flow)
        {
            return flow.stack.GetElementData<Data>(this).data;
        }

        private int GetIdentifier(Flow flow)
        {
            return flow.stack.GetElementData<Data>(this).identifier;
        }

        private void Trigger(GraphReference reference, Notification notification)
        {
            using (var flow = Flow.New(reference))
            {
                var graphData = flow.stack.GetElementData<Data>(this);
                graphData.title = notification.Title;
                graphData.text = notification.Text;
                graphData.data = notification.Data;
                graphData.identifier = notification.Identifier;

                flow.Invoke(received);
            }
        }
    }

}
#endif
