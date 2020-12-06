using Ludiq;
using Bolt;
using UnityEngine;

namespace Dopetools.Animation
{


    [Descriptor(typeof(DopeCurves))]
    public class DopeCurvesUnitDescriptor : UnitDescriptor<DopeCurves>
    {
            public DopeCurvesUnitDescriptor(DopeCurves target) : base(target)
            {
            }


            protected override string DefaultSummary()
            {
                return "A Set of Read-Only AnimationCurves created by a Professional Animator.";
            }

            // Unit Summary
            protected override string DefinedSummary()
            {
                return "A Set of Read-Only AnimationCurves created by a Professional Animator.";
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

            if (port == target.CurveOut)
            {
                description.summary = "Outputs specified AnimationCurve.";
                description.showLabel = false;

            }
        }
    }

}
