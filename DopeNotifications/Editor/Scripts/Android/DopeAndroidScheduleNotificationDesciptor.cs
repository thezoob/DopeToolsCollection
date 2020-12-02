#if UNITY_ANDROID
using Ludiq;
using Bolt;
using UnityEngine;

namespace Dopetools.DopeNotifications
{

    [Descriptor(typeof(DopeAndroidScheduleNotification))]
    public class DopeAndroidScheduleNotificationDescriptor : UnitDescriptor<DopeAndroidScheduleNotification>
    {
        public DopeAndroidScheduleNotificationDescriptor(DopeAndroidScheduleNotification target) : base(target)
        {
        }


        protected override string DefaultSummary()
        {
            return "Schedule Android Notification.";
        }

        // Unit Summary
        protected override string DefinedSummary()
        {
            return "Schedule Android Notification.";
        }

        //Custom Icon
        private Texture2D texture;
        private readonly string icon = "Assets/DopeToolsCollection/DopeNotifications/Editor/Resources/icons/android.png";

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

            if (port == target.Title) description.summary = "Notification Title";
            if (port == target.Text) description.summary = "The Body of the Notification will contain this Text.";
            if (port == target.DateTimeValue) description.summary = "Date and Time. eg. 2/16/2020 12:15:12 PM";

        }
    }

}

#endif