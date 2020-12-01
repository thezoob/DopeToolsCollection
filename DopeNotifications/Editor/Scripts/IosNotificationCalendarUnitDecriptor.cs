using Bolt;
using Ludiq;
using UnityEngine;

namespace Dopetools.DopeNotifications
{

    [Descriptor(typeof(DopeIosNotificationCalendar))]
    public class IosNotificationCalendarUnitDescriptor : UnitDescriptor<DopeIosNotificationCalendar>
    {
        public IosNotificationCalendarUnitDescriptor(DopeIosNotificationCalendar target) : base(target)
        {
        }

        protected override string DefaultSummary()
        {
            return "Schedule IOS Notification.";
        }

        // Unit Summary
        protected override string DefinedSummary()
        {
            return "Schedule IOS Notification.";
        }

        //Custom Icon
        private Texture2D texture;
        private readonly string icon = "Assets/DopeNotifications/Editor/Resources/icons/appleIcon.png";

        protected override EditorTexture DefaultIcon()
        {
            if (texture == null)
            {
                texture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(icon);
            }

            return EditorTexture.Single(texture);
        }

        protected override EditorTexture DefinedIcon()
        {
            if (texture == null)
            {
                texture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(icon);
            }

            return EditorTexture.Single(texture);
        }

        // Port Summary
        protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
        {
            base.DefinedPort(port, description);

            if (port == target.Enter) description.summary = "Enter Flow.";
            if (port == target.Exit) description.summary = "Exit Flow.";
            if (port == target.Identifier) description.summary = "Identifier from Sent Notification";
            if (port == target.Title) description.summary = "Notification Title";
            if (port == target.SubTitle) description.summary = "Notification Sub Title";
            if (port == target.Body) description.summary = "Body Text of the Notification.";
            if (port == target.RepeatsValue) description.summary = "Should the Notification Repeat.";
            if (port == target.DateTimeValue) description.summary = "The Date and Time as a String. e.g. 2/16/2020 12:15:13 PM";




        }


    }

}