using Ludiq;
using Bolt;
using UnityEngine;

    [Descriptor(typeof(DopeAndroidNotificationsStatus))]
    public class DopeAndroidNotificationsStatusDescriptor : UnitDescriptor<DopeAndroidNotificationsStatus>
    {
        public DopeAndroidNotificationsStatusDescriptor(DopeAndroidNotificationsStatus target) : base(target)
        {
        }


        protected override string DefaultSummary()
        {
            return "Events based on Status of Android Notification.";
        }

        // Unit Summary
        protected override string DefinedSummary()
        {
            return "Events based on Status of Android Notification.";
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

            if (port == target.enter) description.summary = "Enter Flow.";
            if (port == target.exit) description.summary = "Exit Flow.";
            if (port == target.Identifier) description.summary = "Identifier from Sent Notification";

        }
    }
