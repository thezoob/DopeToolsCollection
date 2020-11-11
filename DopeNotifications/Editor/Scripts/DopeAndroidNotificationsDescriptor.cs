using Ludiq;
using Bolt;
using UnityEngine;

    [Descriptor(typeof(DopeAndroidNotifications))]
    public class DopeAndroidNotificationsDescriptor : UnitDescriptor<DopeAndroidNotifications>
    {
        public DopeAndroidNotificationsDescriptor(DopeAndroidNotifications target) : base(target)
        {
        }


        protected override string DefaultSummary()
        {
            return "Simplify Android Notifications.";
        }

        // Unit Summary
        protected override string DefinedSummary()
        {
            return "Simplify Android Notifications.";
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
            if (port == target.Delay) description.summary = "Time delay in Seconds.";

        }
    }
