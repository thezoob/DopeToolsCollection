using Ludiq;
using Bolt;
using UnityEngine;

namespace Dopetools.Animation
{


    [Descriptor(typeof(DopeEasingUnit))]
    public class DopeEasingUnitDescriptor : UnitDescriptor<DopeEasingUnit>
    {
        public DopeEasingUnitDescriptor(DopeEasingUnit target) : base(target)
        {
        }


        protected override string DefaultSummary()
        {
            return "Easing Curves made with Code!";
        }

        // Unit Summary
        protected override string DefinedSummary()
        {
            return "Easing Curves made with Code!";
        }

        //Custom Icon
        private Texture2D texture;
        private readonly string icon = "Assets/DopeToolsCollection/DopeCurves/Editor/Resources/DopeCurves.png";

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

            if (port == target.EasedValue)
            {
                description.summary = "Outputs Eased Value.";
                description.showLabel = false;
            }
            else if (port == target.DopeEasingTypeValue)
            {
                description.summary = "Type of Easing Curve to Output.";
                description.showLabel = false;
            }
        }
    }

}
